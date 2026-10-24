using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.EFCore.Model {
    /// <summary>
    /// This class represents a basic table that can be stored in MSSql.
    /// Your EF entity must implement this class in order for the DbContext to handle them.
    /// </summary>
    public class EFEntity : IEFEntity {

        public EFEntity() {
            CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// The Id of the table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The version of the schema of the data
        /// </summary>
        [NotMapped]
        public int Version { get; set; }

        /// <summary>
        /// The user who created this data
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The datetime in UTC at which the data was added.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The user who last updated this data
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// The datetime in UTC at which the data was updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}