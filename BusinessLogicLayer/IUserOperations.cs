using DataAcessLayer.Entity;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IUserOperations
    {
        List<User> GetAllUsers();
        Task AddNote(CreateNoteModel model, int id);
        Task<List<GetAllNotesWithLabelResponse>> GetAllNotes(int userid);
        Task UpdateNoteById(int userid, int id, CreateNoteModel model);
        Task<NotesWithLabelNameModel> GetNoteById(int userid, int id);
        Task<string> DeleteNoteById(int userid, int id);
        Task<string> ArchieveNotebyId(int userid, int id);
        Task<List<UserNotes>> GetAllTrashedNotes(int userid);
        Task<List<UserNotes>> GetAllArchievedNotes(int userid);
        Task<string> AddColorToNote(int userid,int id,string color);
        Task AddLabelToNote(int userid, int noteid, string label);
        Task<string> DeleteLabelByID(int userid, int labelid);
        Task DeleteLabelForNote(int userid, int noteid,string label);




    }
}
