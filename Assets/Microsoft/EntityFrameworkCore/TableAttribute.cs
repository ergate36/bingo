using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations.Schema
{
    //
    // Summary:
    //     Specifies the database table that a class is mapped to.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.Schema.TableAttribute
        //     class using the specified name of the table.
        //
        // Parameters:
        //   name:
        //     The name of the table the class is mapped to.
        public TableAttribute(string name) { }

        //
        // Summary:
        //     Gets the name of the table the class is mapped to.
        //
        // Returns:
        //     The name of the table the class is mapped to.
        //public string Name { get; }
        //
        // Summary:
        //     Gets or sets the schema of the table the class is mapped to.
        //
        // Returns:
        //     The schema of the table the class is mapped to.
        public string Schema { get; set; }
    }
}