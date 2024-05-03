using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Kamus untuk menyimpan pengguna dengan ID unik
        private static Dictionary<int, string> listUsers = new Dictionary<int, string>
        {
            { 0, "Bob" },
            { 1, "Bill" },
            { 2, "Will" },
            { 3, "Ronaldo" }
        };

        // Mendapatkan semua pengguna
        [HttpGet]
        public ActionResult<Dictionary<int, string>> GetUsers()
        {
            return Ok(listUsers);
        }

        // Mendapatkan pengguna berdasarkan ID
        [HttpGet("{id}")]
        public ActionResult<string> GetUser(int id)
        {
            if (listUsers.TryGetValue(id, out var user))
            {
                return Ok(user);
            }
            return NotFound();
        }

        // Menambahkan pengguna baru dengan ID dan nama
        [HttpPost]
        public ActionResult AddUser([FromBody] UserRequest userRequest)
        {
            // Memeriksa apakah ID yang diberikan sudah ada
            if (listUsers.ContainsKey(userRequest.Id))
            {
                // Mengembalikan respons 409 (Conflict) jika ID sudah ada
                return Conflict($"User with ID {userRequest.Id} already exists.");
            }

            // Menambahkan pengguna baru dengan ID dan nama yang diberikan
            listUsers[userRequest.Id] = userRequest.Name;

            // Mengembalikan respons 201 (Created) dengan URI pengguna yang baru ditambahkan
            return CreatedAtAction(nameof(GetUser), new { id = userRequest.Id }, userRequest);
        }

        // Memperbarui pengguna berdasarkan ID
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] string name)
        {
            if (listUsers.ContainsKey(id))
            {
                listUsers[id] = name;
                return NoContent(); // Mengembalikan respons 204 (NoContent) setelah berhasil memperbarui
            }
            return NotFound(); // Jika ID tidak valid
        }

        // Menghapus pengguna berdasarkan ID
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (listUsers.ContainsKey(id))
            {
                listUsers.Remove(id);
                return NoContent(); // Mengembalikan respons 204 (NoContent) setelah berhasil menghapus
            }
            return NotFound(); // Jika ID tidak valid
        }
    }

    // Kelas model untuk merepresentasikan permintaan POST pengguna
    public class UserRequest
    {
        // Properti ID pengguna
        public int Id { get; set; }

        // Properti nama pengguna
        public string Name { get; set; }
    }
}
