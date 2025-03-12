using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Security.Claims;

namespace FundooNotes.Controllers
{
    [Route("api/authoriseduser")]
    [ApiController]
    [Authorize]

    public class AuthorizedUserControl : ControllerBase
    {
        private readonly IUserOperations userOperator;
        public AuthorizedUserControl(IUserOperations userOperator)
        {
            this.userOperator = userOperator;
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<List<User>> GetAll()
        {
            userOperator.GetAllUsers();
            return Ok(userOperator.GetAllUsers());
        }

        //[HttpPost("addnote")]
        //public async Task<ActionResult> AddNotes(CreateNoteModel model)
        //{
        //    try
        //    {
        //        var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        await userOperator.AddNote(model,userID);
        //        return Ok("Note added successfully");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        //[HttpGet("/getallnotes")]
        //public ActionResult<List<GetAllNotesWithLabelResponse>> GetAllNotes()
        //{
        //    try
        //    {
        //        var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        return Ok(userOperator.GetAllNotes(userID));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //}
        //[HttpPut("{userid}/notes/{id}")]
        //public async Task<ActionResult> UpdateNoteById(int userid, int id,CreateNoteModel model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest("Note model is null");
        //    }

        //    try
        //    {
        //        await userOperator.UpdateNoteById(userid, id, model);
        //        return NoContent(); // 204 No Content
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message); // 404 Not Found
        //    }
        //}
        //[HttpGet("notes/{id}")]
        //public async Task<ActionResult> GetNoteById(int id)
        //{
        //    try
        //    {
        //        var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        return Ok(await userOperator.GetNoteById(userid, id));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        //[HttpDelete("notes/{id}")]
        //public async Task<ActionResult> DeleteNoteById(int id)
        //{
        //    try
        //    {
        //        var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        var res = await userOperator.DeleteNoteById(userid,id);
        //        return Ok(res);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

    }
}
