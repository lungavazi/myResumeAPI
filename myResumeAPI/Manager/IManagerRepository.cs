using Microsoft.AspNetCore.Mvc;
using myResumeAPI.Models;

namespace myResumeAPI.Manager
{
    public interface IManagerRepository
    {
        Task<ActionResult> GetAboutme();
    }
}