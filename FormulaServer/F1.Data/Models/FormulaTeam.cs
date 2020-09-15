using F1.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Data.Models
{
    /// <summary>
    /// The class representing the formula team
    /// </summary>
    public class FormulaTeam : IDbEntry
    {
        /// <summary>
        /// The ID of the team.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the team.
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The year the team was founded.
        /// </summary>
        [Required]
        public int YearOfFoundation { get; set; }

        /// <summary>
        /// The number of championships the team has won so far.
        /// </summary>
        [Required]
        public int NumberOfChampionshipsWon { get; set; }

        /// <summary>
        /// Checks if the team has already paied the entry fee to the latests championship or not.
        /// </summary>
        public bool EntryfeePaid { get; set; }
    }
}
