using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorId {  get; set; }

        public string CollaboratorMail {  get; set; }

        [ForeignKey("notesdb")]
        public int UserId {  get; set; }

        [ForeignKey("Notesdb")]
        public int NoteId {  get; set; }

        public DateTime Createdat {  get; set; }
        public DateTime Updatedat { get; set; }

        [JsonIgnore]
        public virtual UserEntity notesdb {  get; set; }
        [JsonIgnore]
        public virtual NoteEntity Notesdb { get; set; }


    }
}
