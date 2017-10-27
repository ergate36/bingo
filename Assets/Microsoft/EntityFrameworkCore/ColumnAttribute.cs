using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations.Schema
{
    //
    // Summary:
    //     Represents the database column that a property is mapped to.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.Schema.ColumnAttribute
        //     class.
        public ColumnAttribute() { }
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.Schema.ColumnAttribute
        //     class.
        //
        // Parameters:
        //   name:
        //     The name of the column the property is mapped to.
        public ColumnAttribute(string name) { }

        //
        // Summary:
        //     Gets the name of the column the property is mapped to.
        //
        // Returns:
        //     The name of the column the property is mapped to.
        //public string Name { get; }
        //
        // Summary:
        //     Gets or sets the zero-based order of the column the property is mapped to.
        //
        // Returns:
        //     The order of the column.
        public int Order { get; set; }
        //
        // Summary:
        //     Gets or sets the database provider specific data type of the column the property
        //     is mapped to.
        //
        // Returns:
        //     The database provider specific data type of the column the property is mapped
        //     to.
        public string TypeName { get; set; }
    }
}