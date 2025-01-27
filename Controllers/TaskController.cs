using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnicaImagineAppTareas.Data;
using pruebaTecnicaImagineAppTareas.Models;
using pruebaTecnicaImagineAppTareas.Request;


namespace pruebaTecnicaImagineAppTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] int? userId, [FromQuery] string? status)
        {
            var query = _context.Tasks.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(t => t.UserId == userId.Value);
            }

            // Si se proporciona status, filtrar por él (sin ser obligatorio)
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status.ToLower() == status.ToLower());
            }

            var tasks = await query.ToListAsync();

            if (!tasks.Any())
            {
                return NotFound(new
                {
                    status = "error",
                    message = "No se encontraron tareas con los criterios especificados.",
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Tareas encontradas exitosamente.",
                data = tasks
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "La tarea no fue encontrada.",
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Tarea encontrada exitosamente.",
                data = new
                {
                    id = task.Id,
                    UserId = task.UserId,
                    title = task.Title,
                    description = task.Description,
                    status = task.Status,
                    dueDate = task.DueDate
                }
            });
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el usuario asociado a la tarea existe
                var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
                if (!userExists)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "El usuario no existe.",
                        data = new { userId = request.UserId }
                    });
                }

                // Crear la nueva tarea
                var task = new Models.Task
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status,
                    DueDate = request.DueDate
                };

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "success",
                    message = "La tarea ha sido creada exitosamente",
                    data = new
                    {
                        id = task.Id,
                        title = task.Title,
                        description = task.Description,
                        status = task.Status,
                        dueDate = task.DueDate
                    }
                });
            }

            return BadRequest(new
            {
                status = "error",
                message = "Error en la solicitud",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateTaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Error en la solicitud",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "La tarea no fue encontrada."
                });
            }

            // Actualizar datos de la tarea
            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.DueDate = request.DueDate;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success",
                message = "La tarea ha sido actualizada exitosamente",
                data = new
                {
                    id = task.Id,
                    title = task.Title,
                    description = task.Description,
                    status = task.Status,
                    dueDate = task.DueDate
                }
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "La tarea no fue encontrada."
                });
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success",
                message = "La tarea ha sido eliminada exitosamente",
                data = new
                {
                    id = task.Id,
                    title = task.Title
                }
            });
        }
    }
}
