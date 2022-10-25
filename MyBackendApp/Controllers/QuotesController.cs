using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DAL;
using MyBackendApp.DTO;
using MyBackendApp.Models;

namespace MyBackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuote _quotes;
        private readonly IMapper _mapper;

        public QuotesController(IQuote quotes, IMapper mapper)
        {
            _quotes = quotes;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<QuotesWithSamuraiDto> GetAll()
        {
            var results = _quotes.GetAll();
            var lstQuotesWithSamuraiDto = _mapper.Map<IEnumerable<QuotesWithSamuraiDto>>(results);
            return lstQuotesWithSamuraiDto;

            /*List<QuotesWithSamuraiDto> lstQuotesWithSamuraiDto = new List<QuotesWithSamuraiDto>();
            var results = _quotes.GetAll();
            foreach(var r in results)
            {
                var quoteDto = new QuotesWithSamuraiDto();
                quoteDto.Id = r.Id;
                quoteDto.Text = r.Text;
                quoteDto.Samurai = new SamuraiGetDTO();
                quoteDto.SamuraiId = r.SamuraiId;
                quoteDto.Samurai.Name = r.Samurai.Name;
                quoteDto.Samurai.Id = r.Samurai.Id;
                lstQuotesWithSamuraiDto.Add(quoteDto);
            }
            return lstQuotesWithSamuraiDto;*/
        }

        [Authorize]
        [HttpGet("Samurai/{samuraiId}")]
        public IEnumerable<QuotesWithSamuraiDto> GetAll(int samuraiId)
        {
            var results = _quotes.GetAll(samuraiId);
            var lstQuotesWithSamuraiDto = _mapper.Map<IEnumerable<QuotesWithSamuraiDto>>(results);
            return lstQuotesWithSamuraiDto;
            /*List<QuotesWithSamuraiDto> lstQuotesWithSamuraiDto = new List<QuotesWithSamuraiDto>();
            var results = _quotes.GetAll(samuraiId);
            foreach (var r in results)
            {
                var quoteDto = new QuotesWithSamuraiDto();
                quoteDto.Id = r.Id;
                quoteDto.Text = r.Text;
                quoteDto.Samurai = new SamuraiGetDTO();
                quoteDto.SamuraiId = r.SamuraiId;
                quoteDto.Samurai.Name = r.Samurai.Name;
                quoteDto.Samurai.Id = r.Samurai.Id;
                lstQuotesWithSamuraiDto.Add(quoteDto);
            }
            return lstQuotesWithSamuraiDto;*/
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {


            //var quoteWithSamuraiDto = new QuotesWithSamuraiDto();
            try
            {
                var result = _quotes.GetById(id);
                var quoteWithSamuraiDto = _mapper.Map<QuotesWithSamuraiDto>(result);
                /*var result = _quotes.GetById(id);
                quoteWithSamuraiDto.Id = result.Id;
                quoteWithSamuraiDto.Text = result.Text;
                quoteWithSamuraiDto.SamuraiId=result.SamuraiId;
                quoteWithSamuraiDto.Samurai = new SamuraiGetDTO();
                quoteWithSamuraiDto.Samurai.Id = result.Samurai.Id;
                quoteWithSamuraiDto.Samurai.Name = result.Samurai.Name;
                return Ok(quoteWithSamuraiDto);*/
                return Ok(quoteWithSamuraiDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(QuoteAddDTO quoteAddDto)
        {
            try
            {
                var newQuote = _mapper.Map<Quote>(quoteAddDto);
                _quotes.Insert(newQuote);

                var quoteGetDto = _mapper.Map<QuoteGetDTO>(newQuote);

                return CreatedAtAction("GetById", new { id = quoteGetDto.Id }, quoteGetDto);

                /*var newQuote = new Quote();
                newQuote.Text = quoteAddDto.Text;
                newQuote.SamuraiId = quoteAddDto.SamuraiId;
                _quotes.Insert(newQuote);
                QuoteGetDTO quoteGetDto = new QuoteGetDTO
                {
                    Id = newQuote.Id,
                    Text = newQuote.Text,
                    SamuraiId = newQuote.SamuraiId
                };
                
                return CreatedAtAction("GetById", new { id = quoteGetDto.Id }, quoteGetDto);*/
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, QuoteAddDTO quoteAddDto)
        {
            try
            {

                var editQuote = _mapper.Map<Quote>(quoteAddDto);
                _quotes.Update(editQuote);
                var quoteGetDto = _mapper.Map<QuoteGetDTO>(editQuote);
                return Ok(quoteGetDto);
                /*var editQuote = new Quote
                {
                    Id = id,
                    Text = quoteAddDto.Text,
                    SamuraiId = quoteAddDto.SamuraiId
                };
                _quotes.Update(editQuote);
                QuoteGetDTO quoteGetDto = new QuoteGetDTO
                {
                    Id = editQuote.Id,
                    Text = editQuote.Text,
                    SamuraiId = editQuote.SamuraiId
                };
                return Ok(quoteGetDto);*/
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _quotes.Delete(id);
                return Ok($"Delete Quote Id: {id} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}