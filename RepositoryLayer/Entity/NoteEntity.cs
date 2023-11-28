using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public DateTime Remainder { get; set; }
        public string Color {  get; set; }
        public string Image {  get; set; }
        public bool Archieve {  get; set; }
        public bool Pin {  get; set; }
        public bool Trash {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}

        [ForeignKey("NotesDb")]
        public int UserId {  get; set; }

        [JsonIgnore]
        public virtual UserEntity NotesDb { get; set; }
    }
}
