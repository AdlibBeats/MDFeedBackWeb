using MDFeedBackWeb.Context;
using MDFeedBackWeb.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace MDFeedBackWeb.Controllers
{
    public class MDFeedBacksController : ApiController
    {
        private readonly MDFeedBackContext _mdFeedBackContext;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MDFeedBacksController() : base() =>
            _mdFeedBackContext = new MDFeedBackContext();

        /// <summary>
        /// Возвращает все MDFeedBacks.
        /// </summary>
        /// <returns>Возвращает все MDFeedBacks.</returns>
        [Route("api/MDFeedBacks")]
        [HttpGet]
        public IEnumerable<MDFeedBackModel> GetMDFeedBacks() =>
            _mdFeedBackContext.MDFeedBacks;

        /// <summary>
        /// Возвращает MDFeedBack по id.
        /// </summary>
        /// <param name="id">Укажите id для поиска MDFeedBack в базе MSSQL.</param>
        /// <returns>Возвращает MDFeedBack по id.</returns>
        [Route("api/MDFeedBacks/{id}")]
        [HttpGet]
        public MDFeedBackModel GetMDFeedBack(string id)
        {
            if (!(int.TryParse(id, out int mdFeedBackModelId)) || mdFeedBackModelId < 1)
                return default(MDFeedBackModel);

            return _mdFeedBackContext
                .MDFeedBacks
                .SingleOrDefault(i => i.MDFeedBackModelId == mdFeedBackModelId);
        }

        /// <summary>
        /// Возвращает последний MDFeedBack.
        /// </summary>
        /// <returns>Возвращает последний MDFeedBack.</returns>
        [Route("api/MDFeedBacks/Last")]
        [HttpGet]
        public MDFeedBackModel GetLastMDFeedBack() =>
            _mdFeedBackContext.MDFeedBacks
                .OrderByDescending(mdFeedBackModel => mdFeedBackModel.MDFeedBackModelId)
                .FirstOrDefault();

        /// <summary>
        /// Возвращает первый MDFeedBack.
        /// </summary>
        /// <returns>Возвращает первый MDFeedBack.</returns>
        [Route("api/MDFeedBacks/First")]
        [HttpGet]
        public MDFeedBackModel GetFirstMDFeedBack() =>
            _mdFeedBackContext.MDFeedBacks.FirstOrDefault();

        /// <summary>
        /// Добавляет MDFeedBack.
        /// </summary>
        /// <param name="mdFeedBackModel">Укажите тело запроса.</param>
        /// <returns>Возвращает IHttpActionResult.</returns>
        [Route("api/MDFeedBacks")]
        [HttpPost]
        public IHttpActionResult CreateMDFeedBack([FromBody]MDFeedBackModel mdFeedBackModel)
        {
            if (mdFeedBackModel == null)
                return BadRequest("Ошибка запроса. Некорректно указано тело запроса.");

            if (string.IsNullOrEmpty(mdFeedBackModel.FirstName) || mdFeedBackModel.FirstName.Length < 2)
                return BadRequest("Ошибка запроса. Укажите ваше имя.");

            if (string.IsNullOrEmpty(mdFeedBackModel.LastName) || mdFeedBackModel.FirstName.Length < 2)
                return BadRequest("Ошибка запроса. Укажите вашу фамилию.");

            if (string.IsNullOrEmpty(mdFeedBackModel.Text))
                return BadRequest("Ошибка запроса. Укажите текст сообщения.");

            _mdFeedBackContext.MDFeedBacks.Add(new MDFeedBackModel
            {
                FirstName = mdFeedBackModel.FirstName,
                LastName = mdFeedBackModel.LastName,
                Text = mdFeedBackModel.Text
            });
            _mdFeedBackContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Редактирует MDFeedBack по id.
        /// </summary>
        /// <param name="id">Укажите id для изменения MDFeedBack в базе MSSQL.</param>
        /// <param name="mdFeedBackModel">Укажите тело запроса.</param>
        /// <returns>Возвращает IHttpActionResult.</returns>
        [Route("api/MDFeedBacks/{id}")]
        [HttpPut]
        public IHttpActionResult EditMDFeedBack(string id, [FromBody]MDFeedBackModel mdFeedBackModel)
        {
            if (!(int.TryParse(id, out int mdFeedBackModelId)) || mdFeedBackModelId < 1)
                return BadRequest($"Ошибка запроса. Некорректно указано поле id:{id}.");

            if (mdFeedBackModel == null)
                return BadRequest("Ошибка запроса. Некорректно указано тело запроса.");

            if (mdFeedBackModelId != mdFeedBackModel.MDFeedBackModelId)
                return BadRequest("Ошибка запроса. Некорректно указано тело запроса.");

            _mdFeedBackContext.Entry(mdFeedBackModel).State = EntityState.Modified;
            _mdFeedBackContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Удаляет MDFeedBack по id.
        /// </summary>
        /// <param name="id">Укажите id для удаления MDFeedBack из базы MSSQL.</param>
        /// <returns>Возвращает IHttpActionResult.</returns>
        [Route("api/MDFeedBacks/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteMDFeedBack(string id)
        {
            if (!(int.TryParse(id, out int mdFeedBackModelId)) || mdFeedBackModelId < 1)
                return BadRequest($"Ошибка запроса. Некорректно указано поле id:{id}.");

            if (!(_mdFeedBackContext.MDFeedBacks
                .SingleOrDefault(i => i.MDFeedBackModelId == mdFeedBackModelId) is MDFeedBackModel mdFeedBackModel))
                return BadRequest("Не удалось найти модель по заданному id");

            _mdFeedBackContext.MDFeedBacks.Remove(mdFeedBackModel);
            _mdFeedBackContext.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _mdFeedBackContext.Dispose();

            base.Dispose(disposing);
        }
    }
}
