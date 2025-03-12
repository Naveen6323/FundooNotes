using DataAcessLayer.Context;
using DataAcessLayer.Entity;
//using DataAcessLayer.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class UserOperations:IUserOperations
    {
        private readonly UserDBContext user;
        public UserOperations(UserDBContext user)
        {
            this.user = user;
        }

        public List<User> GetAllUsers()
        {

            return user.Users.ToList();
        }
        //public async Task AddNote(CreateNoteModel model, int userid)
        //{
        //    var note = new UserNotes
        //    {
        //        Title = model.Title,
        //        Description = model.Description,
        //        Color = model.Color,
        //        IsArchieved = model.IsArchieved,
        //        IsTrash = model.IsTrash,
        //        UserID = userid

        //    };
        //    await user.Notes.AddAsync(note);
        //    await user.SaveChangesAsync();

        //}
        //public async Task<List<GetAllNotesWithLabelResponse>> GetAllNotes(int userid)
        //{
        //    var res = await user.Notes.Where(x => x.UserID == userid).Select(x => x.NoteId).ToListAsync();

        //    List<GetAllNotesWithLabelResponse> result = new List<GetAllNotesWithLabelResponse>();
        //    foreach (var item in res)
        //    {
        //        List<string> names = (from n in user.Notes
        //                              join l in user.Labels on n.NoteId equals l.noteId into labelGroup
        //                              from lg in labelGroup.DefaultIfEmpty() // Right Join
        //                              where n.NoteId == item
        //                              select lg != null ? lg.LabelName : null).ToList();
        //        result.Add(new GetAllNotesWithLabelResponse
        //        {
        //            notetitle = item,
        //            labelIds = names
        //        });
        //    }
        //    return result;

        //    //return user.Notes.Where(x => x.UserID == userid && x.IsArchieved==false  && x.IsTrash==false).ToList();
        //}
        //public async Task UpdateNoteById(int userid, int id, CreateNoteModel model)
        //{
        //    var note = await user.Notes.Where(x => x.UserID == userid && x.NoteId == id).FirstOrDefaultAsync();
        //    if (note != null)
        //    {
        //        note.Title = model.Title;
        //        note.Description = model.Description;
        //        note.Color = model.Color;
        //        note.IsArchieved = model.IsArchieved;
        //        note.IsTrash = model.IsTrash;
        //        await user.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        throw new Exception("Note not found");
        //    }
        //}
        //public async Task<CreateNoteModel> GetNoteById(int userid, int id)
        //{
        //    var note = await user.Notes.Where(x => x.UserID == userid && x.NoteId == id).FirstOrDefaultAsync();
        //    if (note != null)
        //    {
        //        var model = new CreateNoteModel
        //        {
        //            Title = note.Title,
        //            Description = note.Description,
        //            Color = note.Color,
        //            IsArchieved = note.IsArchieved,
        //            IsTrash = note.IsTrash
        //        };
        //        return model;
        //    }
        //    else
        //    {
        //        throw new Exception("Note not found");
        //    }
        //}
        //public async Task<string> DeleteNoteById(int userid, int id)
        //{
        //    var note = await user.Notes.Where(Notes => Notes.UserID == userid && Notes.NoteId == id).FirstOrDefaultAsync();
        //    if (note == null)
        //    {
        //        throw new Exception("Note not found");
        //    }
        //    if (note.IsTrash == true)
        //    {
        //        user.Notes.Remove(note);
        //        await user.SaveChangesAsync();
        //        return "note deelted";
        //    }
        //    else
        //    {
        //        note.IsTrash = true;
        //        await user.SaveChangesAsync();
        //        return "note trashed";
        //    }
        //}
    }
}
