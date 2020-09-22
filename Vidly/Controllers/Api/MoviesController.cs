﻿ 
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq; 
using System.Web.Http; 
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            var dtos = Mapper.Map<List<MovieDto>>(movies); //TODO NOT WORKING
            return Ok(movies);
        }


        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => id == m.Id);
            if (movie == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<MovieDto>(movie);
            return Ok(dto);
        }


        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid || movieDto == null)
            {
                return BadRequest("Movie Format is not Right");
            }
            var movie = Mapper.Map<Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(Request.RequestUri + "/" + movie.Id, movieDto);
        }


        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("movie is not in correct format");
            }

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movie);

            _context.SaveChanges();

            return Ok("updated!!");
        }


        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            
            return Ok("Deleted");
        }

    }
}