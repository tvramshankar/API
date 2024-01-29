using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Dev.Data;
using Dev.Dtos;
using Dev.Migrations;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Mysqlx;
using Mysqlx.Resultset;

public interface ICharecterService
{
    Task<ServiceResponce<CharecterGet>> GetSingle(int id);

    Task<ServiceResponce<List<CharecterGet>>> GetAll();

    Task<ServiceResponce<List<CharecterGet>>> AddCharecter(CharecterPost data);

    Task<ServiceResponce<List<CharecterGet>>> UpdateCharecter(CharecterGet data);

    Task<ServiceResponce<List<CharecterGet>>> DeleteCharecter(int Id);

    Task<ServiceResponce<CharecterGet>> AddCharecterSkills(AddCharecterSkillDto addCharecterSkillDto);
}

public class CharecterService : ICharecterService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _autoMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CharecterService(IMapper autoMapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _autoMapper = autoMapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponce<List<CharecterGet>>> AddCharecter(CharecterPost data)
    {
        var responce = new ServiceResponce<List<CharecterGet>>();
        var charecter = _autoMapper.Map<Rpg>(data);
        GetUserId(out int uId);
        var user = await _dataContext.Users.FirstOrDefaultAsync(e=>e.UserId==uId);
        charecter.User = user;
        _dataContext.Charecter.Add(charecter);
        await _dataContext.SaveChangesAsync();
        GetUserId(out int userId);
        responce.Data = await _dataContext.Charecter.Where(e=>e.User!.UserId == userId)
            .Select(e => _autoMapper.Map<CharecterGet>(e)).ToListAsync();
        responce.Message = "Data Added";
        return responce;
    }

    public async Task<ServiceResponce<CharecterGet>> AddCharecterSkills(AddCharecterSkillDto addCharecterSkillDto)
    {
        var responce = new ServiceResponce<CharecterGet>();
        GetUserId(out int userId);
        var charecter = await _dataContext.Charecter
            .Include(e=>e.weapon)
            .Include(e=>e.Skills)
            .FirstOrDefaultAsync(e=>e.Id == addCharecterSkillDto.CharecterId && e.User!.UserId==userId);
        if(charecter is null)
        {
            responce.Message = $"Charercter with Id {addCharecterSkillDto.CharecterId} not found";
            responce.IsSucess = false;
            return responce;
        }
        var skills = await _dataContext.Skills.FirstOrDefaultAsync(e=>e.Id==addCharecterSkillDto.SkillId);
        if(skills is null)
        {
            responce.Message=$"Skill with skill Id {addCharecterSkillDto.SkillId} is not found";
            responce.IsSucess=false;
            return responce;
        }

        charecter.Skills!.Add(skills);
        await _dataContext.SaveChangesAsync();
        responce.Data = _autoMapper.Map<CharecterGet>(charecter);
        return responce;
    }

    public async Task<ServiceResponce<List<CharecterGet>>> DeleteCharecter(int Id)
    {
        var responce = new ServiceResponce<List<CharecterGet>>();
        try
        {
            GetUserId(out int userId);
            var FetchData = await _dataContext.Charecter.FirstOrDefaultAsync(e => e.Id == Id && e.User!.UserId == userId);
            if (FetchData is null)
                throw new Exception($"Not found Id {Id}");
            _dataContext.Charecter.Remove(FetchData);
            await _dataContext.SaveChangesAsync();
            responce.Data = _autoMapper.Map<List<CharecterGet>>(_dataContext.Charecter);
        }
        catch (Exception ex)
        {
            responce.IsSucess = false;
            responce.Message = $"{ex.Message}";
        }
        return responce;
    }

    public async Task<ServiceResponce<List<CharecterGet>>> GetAll()
    {
        var responce = new ServiceResponce<List<CharecterGet>>();
        GetUserId(out int userId);
        responce.Data = _autoMapper.Map<List<CharecterGet>>(await _dataContext.Charecter
            .Include(e=>e.Skills)
            .Include(e=>e.weapon)
            .Where(e=>e.User!.UserId==userId).ToListAsync());
        return responce;
    }

    public async Task<ServiceResponce<CharecterGet>> GetSingle(int id)
    {
        var responce = new ServiceResponce<CharecterGet>();
        try
        {
            GetUserId(out int userId);
            var data = await _dataContext.Charecter.FirstOrDefaultAsync(e => e.Id == id && e.User!.UserId==userId);
            if (data is null)
                throw new Exception($"Entry with Id:{id} not found");
            responce.Data = _autoMapper.Map<CharecterGet>(data);
        }

        catch(Exception ex)
        {
            responce.Message = ex.Message;
            responce.IsSucess = false;
        }


        return responce;
    }

    public async Task<ServiceResponce<List<CharecterGet>>> UpdateCharecter(CharecterGet data)
    {
        var responce = new ServiceResponce<List<CharecterGet>>();
        try
        {

            var FetchData = await _dataContext.Charecter
            .Include(e=>e.User)
            .FirstOrDefaultAsync(e => e.Id == data.Id);
            GetUserId(out int userId);
            if (FetchData is null ||  !(FetchData.User!.UserId==userId))
                throw new Exception($"Not found Id {data.Id}");
                //these comments are important. have multiple methods to update db using ef core
            // var isCached = _dataContext.Charecter.Local.FirstOrDefault(e => e.Id == data.Id);
            // if (isCached is not null)
            //     _dataContext.Entry(FetchData).State = EntityState.Detached;//when data context used more than one there is possiblitily of datacontext to track on existing entity with same id so error will come.
            // _dataContext.Entry(_autoMapper.Map<Rpg>(data)).State = EntityState.Modified;
            FetchData.person = data.person;
            FetchData.Defence = data.Defence;
            FetchData.Intelligence = data.Intelligence;
            FetchData.Name = data.Name;
            FetchData.HitPoints = data.HitPoints;
            await _dataContext.SaveChangesAsync();

            //_dataContext.Entry(FetchData).State = EntityState.Detached; !important.

            // Possible Error : the instance of entity type 'rpg' cannot be tracked because another instance 
            //with the same key value for {'id'} is already being tracked. when attaching existing entities,
            // ensure that only one entity instance with a given key value is attached. 
            //consider using 'dbcontextoptionsbuilder.enablesensitivedatalogging' to see the conflicting key values.
            // var isCaching = _dataContext.Charecter.Local.FirstOrDefault(e=>e.Id == data.Id);
            // if(isCaching is not null)
            //     _dataContext.Entry(FetchData).State = EntityState.Detached;

            //************ To update all *********
            //_dataContext.Charecter.Update(_autoMapper.Map<Rpg>(data));


            //************ To update single column *********
            //await _dataContext.Charecter.Where(e=>e.Id==data.Id).ExecuteUpdateAsync(e=> e.SetProperty(f=>f.SchoolName,data.SchoolName)); here no need for savechangeasync
            //Another method most efficient is using Attach
            // _dataContext.Charecter.Attach(FetchData);
            // FetchData.SchoolName = data.SchoolName;
            // await _dataContext.SaveChangesAsync();
            //Another method most efficient is using Attach
            // _dataContext.Charecter.Attach(_autoMapper.Map<Rpg>(data));
            // _dataContext.Entry(data).Property(e=>e.SchoolName).IsModified = true;
            // await _dataContext.SaveChangesAsync();

            await _dataContext.SaveChangesAsync();
            responce.Data = _autoMapper.Map<List<CharecterGet>>(_dataContext
                .Charecter.Where(e=>e.User!.UserId==userId));
        }
        catch (Exception ex)
        {
            responce.IsSucess = false;
            responce.Message = $"{ex.Message}";
        }
        return responce;
    }

    private void GetUserId(out int UserId)
    {
       bool IsPassable = int.TryParse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier),out UserId);
       if(!IsPassable)
        throw new Exception("UserId cant be found from claims");
    }
}