using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DAL;
using MyBackendApp.DTO;
using MyBackendApp.Models;

namespace MyBackendApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samurai;
        private readonly IMapper _mapper;
        public SamuraisController(ISamurai samurai, IMapper mapper)
        {
            _samurai = samurai;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<SamuraiGetDTO> Get()
        {
            var results = _samurai.GetAll();
            var lstSamuraiGetDto = _mapper.Map<IEnumerable<SamuraiGetDTO>>(results);
            return lstSamuraiGetDto;
            /*List<SamuraiGetDTO> lstSamuraiGetDto = new List<SamuraiGetDTO>();
            var results = _samurai.GetAll();
            foreach(var s in results)
            {
                lstSamuraiGetDto.Add(new SamuraiGetDTO
                {
                    Id = s.Id,
                    Name = s.Name
                });
            }
            return lstSamuraiGetDto;*/
        }

        [HttpGet("WithQuotes")]
        public IEnumerable<SamuraiWithQuoteDTO> GetSamuraiWithQuote()
        {
            var results = _samurai.GetAllWithQuote();
            var lstSamuraiWithQuoteDto = _mapper.Map<IEnumerable<SamuraiWithQuoteDTO>>(results);
            return lstSamuraiWithQuoteDto;
            /*List<SamuraiWithQuoteDTO> lstSamuraiWithQuoteDto = new List<SamuraiWithQuoteDTO>();
            var results = _samurai.GetAllWithQuote();
            foreach(var r in results)
            {
                List<QuoteGetDTO> lstQuoteGetDto = new List<QuoteGetDTO>();
                foreach(var q in r.Quotes)
                {
                    lstQuoteGetDto.Add(new QuoteGetDTO
                    {
                        Id = q.Id,
                        Text = q.Text,
                        SamuraiId = q.SamuraiId
                    });
                }
                lstSamuraiWithQuoteDto.Add(new SamuraiWithQuoteDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Quotes = lstQuoteGetDto
                });
            }
            return lstSamuraiWithQuoteDto;*/
        }

        [HttpGet("{id}")]
        public SamuraiGetDTO Get(int id)
        {
            var result = _samurai.GetById(id);
            var samuraiGetDto = _mapper.Map<SamuraiGetDTO>(result);
            return samuraiGetDto;
            /*var result = _samurai.GetById(id);
            var samuraiGetDto = new SamuraiGetDTO
            {
                Name = result.Name
            };
            return samuraiGetDto;*/
        }

        [HttpGet("ByName")]
        public IEnumerable<SamuraiGetDTO> GetByName(string name)
        {
            var results = _samurai.GetByName(name);
            var listSamuraiGetDto = _mapper.Map<IEnumerable<SamuraiGetDTO>>(results);
            return listSamuraiGetDto;
            /*List<SamuraiGetDTO> listSamuraiGetDto = new List<SamuraiGetDTO>();
            var results = _samurai.GetByName(name);
            foreach(var r in results)
            {
                listSamuraiGetDto.Add(new SamuraiGetDTO
                {
                    Name = r.Name
                });
            }
            return listSamuraiGetDto;*/
        }

        [HttpPost("WithQuotes")]
        public IActionResult PostWithQuotes(SamuraiAddWithQuotesDto samuraiWithQuotesDto)
        {
            try
            {
                var samurai = _mapper.Map<Samurai>(samuraiWithQuotesDto);
                var newSamurai = _samurai.Insert(samurai);
                var samuraiGetDto = _mapper.Map<SamuraiGetDTO>(newSamurai);
                return CreatedAtAction("Get", new { id = samuraiGetDto.Id }, samuraiGetDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(SamuraiAddDTO samuraiDto)
        {
            try
            {
                var samurai = _mapper.Map<Samurai>(samuraiDto);
                var newSamurai = _samurai.Insert(samurai);
                var samuraiGetDto = _mapper.Map<SamuraiGetDTO>(newSamurai);
                return CreatedAtAction("Get", new { id = samuraiGetDto.Id }, samuraiGetDto);

                /*var samurai = new Samurai
                {
                    Name = samuraiDto.Name
                };
                var newSamurai = _samurai.Insert(samurai);
                var samuraiGetDto = new SamuraiGetDTO
                {
                    Id = newSamurai.Id,
                    Name = newSamurai.Name
                };
                return CreatedAtAction("Get", new { id = samuraiGetDto.Id }, samuraiGetDto);*/
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(int id, SamuraiAddDTO samuraiDto)
        {
            try
            {
                var samurai = _mapper.Map<Samurai>(samuraiDto);
                var editSamurai = _samurai.Update(samurai);
                var samuraiGetDto = _mapper.Map<SamuraiGetDTO>(editSamurai);
                return Ok(samuraiGetDto);

                /*var samurai = new Samurai
                {
                    Id = id,
                    Name = samuraiDto.Name
                };
                var editSamurai = _samurai.Update(samurai);
                SamuraiGetDTO samuraiGetDto = new SamuraiGetDTO
                {
                    Id = id,
                    Name = samuraiDto.Name
                };
                return Ok(samuraiGetDto);*/
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
                _samurai.Delete(id);
                return Ok($"Delete id {id} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Battle")]
        public IActionResult AddSamuraiToBattle(AddSamuraiToBattleDto addSamuraiToBattleDto)
        {
            try
            {
                _samurai.AddSamuraiToBattle(addSamuraiToBattleDto.SamuraiId, addSamuraiToBattleDto.BattleId);
                return Ok($"Samurai id {addSamuraiToBattleDto.SamuraiId} berhasil dittambahkan ke battle {addSamuraiToBattleDto.BattleId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Horse")]
        public IActionResult AddHorse(AddHorseDto addHorseDto)
        {
            try
            {
                var horse = _mapper.Map<Horse>(addHorseDto);
                _samurai.AddHorse(horse);
                return Ok(horse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("WithHorse")]
        public IEnumerable<SamuraiWithHorseDto> GetSamuraiWithHorse()
        {
            var samurais = _samurai.GetAllSamuraiWithHorse();
            var samuraisWithHorse = _mapper.Map<IEnumerable<SamuraiWithHorseDto>>(samurais);
            return samuraisWithHorse;
        }

        [HttpGet("WithBattle/{samuraiId}")]
        public SamuraiWithBattleDto GetSamuraiWithBattle(int samuraiId)
        {
            var samurais = _samurai.GetSamuraiWithBattle(samuraiId);
            var samuraiWithBattleDto = _mapper.Map<SamuraiWithBattleDto>(samurais);
            return samuraiWithBattleDto;
        }

        [HttpGet("WithBattles")]
        public IEnumerable<SamuraiWithBattleDto> GetAllSamuraisWithBattles()
        {
            var samurais = _samurai.GetAllSamuraisWithBattles();
            var samuraiWithBattles = _mapper.Map<IEnumerable<SamuraiWithBattleDto>>(samurais);
            return samuraiWithBattles;
        }

        [HttpPost("RemoveBattle")]
        public IActionResult RemoveBattleFromSamurai(AddSamuraiToBattleDto samuraiBattleDto)
        {
            try
            {
                _samurai.RemoveBattleFromSamurai(samuraiBattleDto.SamuraiId, samuraiBattleDto.BattleId);
                return Ok($"Remove battle {samuraiBattleDto.BattleId} from Samurai {samuraiBattleDto.SamuraiId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SaidWord")]
        public IEnumerable<SamuraiGetDTO> GetSamuraiWhoSaidWord(string text)
        {
            var samurais = _samurai.GetSamuraiWhoSaidWord(text);
            var samuraiGetDto = _mapper.Map<IEnumerable<SamuraiGetDTO>>(samurais);
            return samuraiGetDto;
        }

        [HttpDelete("Quotes/{samuraiId}")]
        public IActionResult RemoveQuotesFromSamurai(int samuraiId)
        {
            try
            {
                _samurai.RemoveQuotesFromSamurai(samuraiId);
                return Ok($"Quotes dari samurai id {samuraiId} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}