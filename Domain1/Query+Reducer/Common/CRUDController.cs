
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CRUDController<TEntity> : Controller
    where TEntity : class, IEntity
{
    protected readonly MainDbContext db;

    public CRUDController(MainDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public virtual IEnumerable<TEntity> GetList(int skip = 0, int take = 100)
    {
        var filters = GetFilters(Request.Query);

        return db.Set<TEntity>()
            .AsNoTracking()
            .Where(x => FilterEntityBeforeSelect(filters, x))
            .Skip(skip)
            .Take(take)
            .ToList()
            .Where(x => FilterEntityAfterSelect(Request.Query, filters, x));
    }

    [HttpGet("count")]
    public virtual long GetCount()
    {
        var filters = GetFilters(Request.Query);

        return db.Set<TEntity>()
            .AsNoTracking()
            .Where(x => FilterEntityBeforeSelect(filters, x))
            .Count();
    }


    [HttpGet("{id}")]
    public virtual TEntity Get(Guid? id)
    {
        var filters = GetFilters(Request.Query);

        var result = db.Set<TEntity>()
               .Where(x => FilterEntityBeforeSelect(filters, x))
               .AsNoTracking()
               .FirstOrDefault(x => x.Id == id);

        if (!FilterEntityAfterSelect(Request.Query, filters, result))
            return null;

        return result;

    }


    [HttpPost]
    public virtual TEntity Create(TEntity entity)
    {
        db.Set<TEntity>().Add(entity);
        db.SaveChanges();

        return entity;
    }


    [HttpDelete("{id}")]
    public virtual TEntity Delete(Guid? id)
    {
        var entity = db.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        if (entity != null)
        {
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
        }

        return entity;
    }


    protected virtual TEntity GetFilters(IQueryCollection query)
    {
        return null;
    }

    protected virtual bool FilterEntityBeforeSelect(TEntity filter, TEntity entity)
    {
        return true;
    }

    protected virtual bool FilterEntityAfterSelect(IQueryCollection query, TEntity filters, TEntity entity)
    {
        return true;
    }
}