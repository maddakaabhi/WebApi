using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId {  get; set; }
        public string LabelName { get; set; }

        [ForeignKey("Notesdb")]
        public int Id {  get; set; }
        [ForeignKey("notesDb")]
        public int UserId {  get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime UpdatedAt { get; set;}

        [JsonIgnore]
        public virtual UserEntity notesDb { get; set; }

        [JsonIgnore]
        public virtual NoteEntity Notesdb { get; set;}
    }
}
