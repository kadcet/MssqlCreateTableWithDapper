using MsSqlCreateTableWithDapper.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ForeignKeyAttribute = MsSqlCreateTableWithDapper.Model.Attributes.ForeignKeyAttribute;

namespace MsSqlCreateTableWithDapper.Model.Helper
{
    public class MssqlSqlBuilder
    {

        public string GenerateInsertSqlStatement(Type model)
        {
            /*
              @"INSERT INTO Users ([UserName],[Password],[Email],[PhoneNumber],[CreatedDateTime],[CreatedBy],[UpdateBy],[UpdateDateTime],[DeletedBY],[DeletedDateTime],[Status]) 
                      VALUES ('Umut Ergün','sfire alanı','uergun04@gmail.com','565456465','2024-02-15 16:21',1,0,'1910-01-01',0,'1910-01-01',1);
                      SELECT SCOPE_IDENTITY(); ";
            */

            StringBuilder insertStatement = new StringBuilder($"INSERT INTO {model.Name} (");                    // INSERT INTO Users (

            PropertyInfo[] properties = model.GetProperties(); // property leri çektim

            foreach (PropertyInfo property in properties)
            {
                var clmAttr = property.GetCustomAttribute(typeof(CustomColumnAttribute)) as CustomColumnAttribute;
                var dbIgnoreColumn = property.GetCustomAttribute(typeof(DbIgnoreColumnAttribute)) as DbIgnoreColumnAttribute;

                if (dbIgnoreColumn is { DbIgnore: true }) continue;
                if (clmAttr is { IsPrimaryKey: true }) continue;

                insertStatement.Append($"[{property.Name}],"); // INSERT INTO Users ([Username],
            }

            insertStatement.Length -= 1;
            insertStatement.Append(") VALUES ("); // INSERT INTO Users ([UserName],[Password],[Email],[PhoneNumber],[CreatedDateTime],[CreatedBy],[UpdateBy],[UpdateDateTime],[DeletedBY],[DeletedDateTime],[Status]) VALUES ( 


            foreach (PropertyInfo property in properties)
            {
                var clmAttr = property.GetCustomAttribute(typeof(CustomColumnAttribute)) as CustomColumnAttribute;
                var dbIgnoreColumn = property.GetCustomAttribute(typeof(DbIgnoreColumnAttribute)) as DbIgnoreColumnAttribute;

                if (dbIgnoreColumn is { DbIgnore: true }) continue;
                if (clmAttr is { IsPrimaryKey: true }) continue;

                insertStatement.Append($"@{property.Name},"); // INSERT INTO Users ([UserName],[Password],[Email],[PhoneNumber],[CreatedDateTime],[CreatedBy],[UpdateBy],[UpdateDateTime],[DeletedBY],[DeletedDateTime],[Status]) VALUES (@UserName
            }

            insertStatement.Length -= 1;
            insertStatement.Append(");"); // INSERT INTO Users ([UserName],[Password],[Email],[PhoneNumber],[CreatedDateTime],[CreatedBy],[UpdateBy],[UpdateDateTime],[DeletedBY],[DeletedDateTime],[Status]) VALUES (@UserName,@Password,@Email,@PhoneNumber,@CreatedDateTime,@CreatedBy,@UpdateBy,@UpdateDateTime,@DeletedBY,@DeletedDateTime,@Status);

            insertStatement.Append("SELECT SCOPE_IDENTITY();");// INSERT INTO Users ([UserName],[Password],[Email],[PhoneNumber],[CreatedDateTime],[CreatedBy],[UpdateBy],[UpdateDateTime],[DeletedBY],[DeletedDateTime],[Status]) VALUES (@UserName,@Password,@Email,@PhoneNumber,@CreatedDateTime,@CreatedBy,@UpdateBy,@UpdateDateTime,@DeletedBY,@DeletedDateTime,@Status); SELECT SCOPE_IDENTITY();
            return insertStatement.ToString();
        }

        public string GenerateUpdateSqlStatement(Type model, string updateOnlyThisFields = "")
        {

            var properties = model.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(z => z.CanRead && z.CanWrite).Where(z => z.GetGetMethod(true)!.IsPublic)
                .Where(z => z.GetSetMethod(true)!.IsPublic).ToList();
            string tableName = model.Name;
            var setClauses = new List<string>();

            string primaryKeyPropertyName = "";

            var fieldss = new List<string>();
            if (!string.IsNullOrEmpty(updateOnlyThisFields)) fieldss = updateOnlyThisFields.Split(',').ToList();

            foreach (PropertyInfo property in properties)
            {
                var clmAttr = property.GetCustomAttribute(typeof(CustomColumnAttribute)) as CustomColumnAttribute;
                var dbIgnoreColumn = property.GetCustomAttribute(typeof(DbIgnoreColumnAttribute)) as DbIgnoreColumnAttribute;

                if (dbIgnoreColumn is { DbIgnore: true }) continue;

                if (clmAttr is { IsPrimaryKey: true })
                {
                    primaryKeyPropertyName = property.Name;
                    continue;
                }


                if (property.PropertyType.FullName == null) continue;
                if (property.PropertyType.BaseType?.FullName == null) continue;


                if (!property.PropertyType.FullName.Contains("Int32") &&
                    !property.PropertyType.FullName.Contains("String") &&
                    !property.PropertyType.FullName.Contains("DateTime") &&
                    !property.PropertyType.FullName.Contains("Decimal") &&
                    !property.PropertyType.FullName.Contains("Boolean") &&
                    !property.PropertyType.FullName.Contains("Double") &&
                    !property.PropertyType.FullName.Contains("Float") &&
                    !property.PropertyType.BaseType.FullName.Contains("Enum")

                    )
                    continue;

                if (fieldss.Count > 0) if (!fieldss.Contains(property.Name)) continue;

                setClauses.Add(property.Name + "=@" + property.Name);
            }

            // Construct the full SQL command
            string setClause = string.Join(",", setClauses);
            return $"UPDATE {tableName} SET {setClause} WHERE {primaryKeyPropertyName}=@{primaryKeyPropertyName}";
        }

        //TABLO EKLEME
        public string GenerateCreateTableStatement(Type model)
        {
            // stringbuildere create tablo +modelin name ini alıp ekledik (
            StringBuilder createTableQuery = new StringBuilder($"CREATE TABLE [{model.Name}] (");
            PropertyInfo[] properties = model.GetProperties(); // bütün properyleri çekip arry a attık

            foreach (PropertyInfo property in properties) // modeldeki propery leri dönüyoruz
            {
                #region Ignore attributeyi ayıkladık
                // bizim hazırladığımız custom attributeyi ismini vererek  değişkene atıyoruz. burada sadece DbIgnoreColumnAttribute kullanan propertyleri alıyoruz.
                // bu bize yoksa null döner varsa belirtiğimiz attribute tipini geri döner
                var dbIgnoreColumn = property.GetCustomAttribute(typeof(DbIgnoreColumnAttribute)) as DbIgnoreColumnAttribute;

                // bu attribute sahip olan propery databasede oluşturulmaz.altta bu attribute olan property i continue ile es geçiyoruz.
                if (dbIgnoreColumn is { DbIgnore: true }) continue;


                #endregion


                string columnName = property.Name; // foreachtan gelen her property nin ismini string olarak değişkene atıyoruz.
                //  şimdilik bu değerleri atıyoruz
                string? columnType = null;
                int length = 255;
                bool isPrimaryKey = false;
                bool allowNull = true;


                // Check if the property has a CustomColumn attribute for additional customization
                //var columnAttributes = property.GetCustomAttribute(typeof(CustomColumnAttribute)) as CustomColumnAttribute;

                if (property.IsDefined(typeof(CustomColumnAttribute))) // CustomColumnAttribute tipinde custom Attribute var mı
                {
                    var columnAttributes = property.GetCustomAttributes(typeof(CustomColumnAttribute)); // varsa CustomColumnAttribute alan propertyleri değişkene collection olarak at
                    foreach (CustomColumnAttribute columnAttribute in columnAttributes) // burda dön
                    {
                        if (columnAttribute.IsPrimaryKey) // IsPrimaryKey i true olanı geldiğinde yukarıdaki isPrimaryKey=false yi true ye çevir
                        {
                            isPrimaryKey = true;

                        }
                        else
                        {
                            columnType = columnAttribute.TypeName; // IsPrimaryKey yoksa property nin tipini ata null olarak atayacak
                            length = columnAttribute.Length;
                        }
                        if (columnAttribute.AllowNull)
                        {
                            allowNull = true;
                        }
                        else
                        {
                            allowNull = false;
                        }
                    }
                }

                if (columnType == null)
                {
                    // null olarak atananlara GetMySqlTypeFromCSharpType metodu ile typename ataması yapıyoruz.
                    columnType = GetMySqlTypeFromCSharpType(property.PropertyType);
                }

                // Append the length to columnType if it's a VARCHAR type
                if (columnType == "nvarchar")
                {
                    // tipi nvarchar olanlara değer aralığını üstteki length değişkeninin değerini veriyoruz
                    columnType += $"({length})";
                }

                createTableQuery.Append($"[{columnName}] {columnType}");

                if (isPrimaryKey)
                {
                    //columnType = GetMySqlTypeFromCSharpType(property.PropertyType);
                    createTableQuery.Append($" IDENTITY(1,1) NOT NULL");
                    createTableQuery.Append(",");
                    continue;
                }

                if (!allowNull)
                {

                    createTableQuery.Append(" NOT NULL");
                }

                else
                {
                    createTableQuery.Append(" NULL");
                }
                createTableQuery.Append(",");



            }


            // Remove the last comma and close the statement
            //createTableQuery.Length -= 1;
            createTableQuery.Append($"CONSTRAINT PK_{model.Name} PRIMARY KEY (Id)");
            createTableQuery.Append(")");

            return createTableQuery.ToString();
        }

        public string GenerateCreateForeignKey(Type model)
        {
            //alter table MyTable  add constraint MyTable_MyColumn_FK FOREIGN KEY(MyColumn) references MyOtherTable(PKColumn)

            // stringbuildere create tablo +modelin name ini alıp ekledik (
            StringBuilder createTableQuery = new StringBuilder();
            PropertyInfo[] properties = model.GetProperties(); // bütün properyleri çekip arry a attık

            var foreignKeyExist = false;

            foreach (PropertyInfo property in properties) // modeldeki propery leri dönüyoruz
            {
                #region Ignore attributeyi ayıkladık
                // bizim hazırladığımız custom attributeyi ismini vererek  değişkene atıyoruz. burada sadece DbIgnoreColumnAttribute kullanan propertyleri alıyoruz.
                // bu bize yoksa null döner varsa belirtiğimiz attribute tipini geri döner
                var dbIgnoreColumn = property.GetCustomAttribute(typeof(DbIgnoreColumnAttribute)) as DbIgnoreColumnAttribute;
                // bu attribute sahip olan propery databasede oluşturulmaz.altta bu attribute olan property i continue ile es geçiyoruz.
                if (dbIgnoreColumn is { DbIgnore: true }) continue;
                #endregion


                string columnName = property.Name; // foreachtan gelen her property nin ismini string olarak değişkene atıyoruz.


                // Check if the property has a ForeignKey attribute for foreign key constraints
                if (property.IsDefined(typeof(ForeignKeyAttribute)))
                {
                    if (property.GetCustomAttribute(typeof(ForeignKeyAttribute)) is ForeignKeyAttribute foreignKeyAttribute)
                    {
                        foreignKeyExist = true;
                        createTableQuery.Append(@$"ALTER TABLE [{model.Name}] 
                                                   Add CONSTRAINT [FK_{model.Name}_{columnName}_{foreignKeyAttribute.ReferenceTable}_{foreignKeyAttribute.ReferenceColumn}] 
                                                   FOREIGN KEY ({columnName}) REFERENCES {foreignKeyAttribute.ReferenceTable} ({foreignKeyAttribute.ReferenceColumn});");

                        //  constraint fk_name foreign key (cityId) references city (id)
                    }

                }


            }

            return foreignKeyExist ? createTableQuery.ToString() : "";
        }

        public static string GetMySqlTypeFromCSharpType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return "bit";
                case TypeCode.Char:
                    return "char(1)";
                case TypeCode.SByte:
                case TypeCode.Byte:
                    return "tinyint";
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    return "smallint";
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return "int";
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return "bigint";
                case TypeCode.Single:
                    return "float";
                case TypeCode.Double:
                    return "float";
                case TypeCode.Decimal:
                    return "decimal(18,0)";
                case TypeCode.DateTime:
                    return "datetime";
                case TypeCode.String:
                    return "nvarchar"; // Removed the default length
                default:
                    {
                        if (type == typeof(byte[]))
                            return "VARBINARY(8000)";
                        else if (type == typeof(Nullable<Char>))
                        {
                            return "CHAR(1) NULL";
                        }
                        else if (type == typeof(Nullable<SByte>) || type == typeof(Nullable<Byte>))
                        {
                            return "TINYINT NULL";
                        }
                        else if (type == typeof(Nullable<Int16>) || type == typeof(Nullable<UInt16>))
                        {
                            return "SMALLINT NULL";
                        }
                        else if (type == typeof(Nullable<Int32>) || type == typeof(Nullable<UInt32>))
                        {
                            return "INT NULL";
                        }
                        else if (type == typeof(Nullable<Int64>) || type == typeof(Nullable<UInt64>))
                        {
                            return "BIGINT NULL";
                        }
                        else if (type == typeof(Nullable<Single>))
                        {
                            return "FLOAT NULL";
                        }
                        else if (type == typeof(Nullable<Double>))
                        {
                            return "DOUBLE NULL";
                        }
                        else if (type == typeof(Nullable<Decimal>))
                        {
                            return "DECIMAL(18,0) NULL";
                        }
                        else if (type == typeof(Nullable<DateTime>))
                        {
                            return "DATETIME NULL";
                        }
                        else if (type == typeof(Nullable<bool>))
                        {
                            return "BOOLEAN NULL";
                        }
                        else
                            throw new Exception(type.Name + " is unaccounted for.");
                    }
            }
        }


    }
}
