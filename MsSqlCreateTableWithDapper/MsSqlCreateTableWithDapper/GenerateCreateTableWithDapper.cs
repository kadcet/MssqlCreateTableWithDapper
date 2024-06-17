using MsSqlCreateTableWithDapper.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using ForeignKeyAttribute = MsSqlCreateTableWithDapper.Attributes.ForeignKeyAttribute;

namespace MsSqlCreateTableWithDapper
{
    public class GenerateCreateTableWithDapper
    {

        //   string createTableQuery = GenerateCreateTableStatement(typeof(Users));

        public string GenerateCreateTableStatement(Type model)
        {
            
            StringBuilder createTableQuery = new StringBuilder($"CREATE TABLE [{model.Name}] (");
            PropertyInfo[] properties = model.GetProperties();  

            foreach (PropertyInfo property in properties) 
            {
                #region Ignore attributeyi ayıkladık
               
                var dbIgnoreColumn = property.GetCustomAttribute(typeof(DbIgnoreColumnAttribute)) as DbIgnoreColumnAttribute;
                if (dbIgnoreColumn is { DbIgnore: true }) continue;
                #endregion


                string columnName = property.Name;
                string? columnType = null;
                int length = 255;
                bool isPrimaryKey = false;
                bool allowNull = true;



                if (property.IsDefined(typeof(CustomColumnAttribute))) 
                {
                    var columnAttributes = property.GetCustomAttributes(typeof(CustomColumnAttribute)); 
                    foreach (CustomColumnAttribute columnAttribute in columnAttributes) 
                    {
                        if (columnAttribute.IsPrimaryKey) 
                        {
                            isPrimaryKey = true;

                        }
                        else
                        {
                            columnType = columnAttribute.TypeName; 
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
                    columnType = GetMsSqlType(property.PropertyType);
                }

                if (columnType == "nvarchar")
                {
                    columnType += $"({length})";
                }

                createTableQuery.Append($"[{columnName}] {columnType}");

                if (isPrimaryKey)
                {
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


        public static string GetMsSqlType(Type type)
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
                    return "nvarchar"; 
              
            }
            throw new Exception(type.Name + " dönüştürülemedi.");
        }

    }
}
