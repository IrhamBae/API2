using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MahasiswaController : ControllerBase
    {
        private static List<Mahasiswa> mhs = new List<Mahasiswa>();

        private readonly ILogger<MahasiswaController> _logger;

        public MahasiswaController(ILogger<MahasiswaController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMahasiswa")]
        public IEnumerable<Mahasiswa> Get()
        {
            return Enumerable.Range(1, mhs.Count).Select(i => new Mahasiswa("", "")
            {
                Nama = mhs[i-1].Nama,
                Nim = mhs[i-1].Nim,
            }).ToArray();
        }

        [HttpPost(Name = "PostMahasiswa")]
        public ActionResult Post(string nama, string nim)
        {
            Mahasiswa mahasiswa = new Mahasiswa(nama, nim);
            mhs.Add(mahasiswa);
            return CreatedAtAction(nameof(Get), new { id = mhs.IndexOf(mahasiswa) }, mahasiswa);
        }

        [HttpDelete(Name = "DeleteMahasiswa")]
        public ActionResult Delete(string nama)
        {
            int idx = 0;
            while (mhs[idx].Nama != nama)
            {
                idx++;
            }
            mhs.RemoveAt(idx);
            return NoContent();
        }

        [HttpGet("{idx}")]
        public Mahasiswa Get(int idx)
        {
            return mhs[idx];
        }
    }
}
