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
        //Task AddNote(CreateNoteModel model, int id);
        ////Task<List<GetAllNotesWithLabelResponse>> GetAllNotes(int userid);
        //Task UpdateNoteById(int userid, int id, CreateNoteModel model);
        //Task<CreateNoteModel> GetNoteById(int userid, int id);
        //Task<string> DeleteNoteById(int userid, int id);



    }
}
