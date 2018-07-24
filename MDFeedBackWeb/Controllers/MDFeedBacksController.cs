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
        /// Возвращает MDFeedBack.
        /// </summary>
        /// <returns>Возвращает MDFeedBack.</returns>
        [Route("api/MDFeedBacks")]
        [HttpGet]
        public IEnumerable<MDFeedBackModel> GetMDFeedBacks() =>
            _mdFeedBackContext.MDFeedBacks;

        /// <summary>
        /// Возвращает MDFeedBack.
        /// </summary>
        /// <param name="id">Укажите id для поиска MDFeedBack в базы MSSQL.</param>
        /// <returns>Возвращает MDFeedBack.</returns>
        [Route("api/MDFeedBacks/{id}")]
        [HttpGet]
        public MDFeedBackModel GetMDFeedBack(string id)
        {
            if (!(int.TryParse(id, out int mdFeedBackModelId)))
                return default(MDFeedBackModel);

            return _mdFeedBackContext
                .MDFeedBacks
                .SingleOrDefault(i => i.MDFeedBackModelId == mdFeedBackModelId);
        }

        /// <summary>
        /// Добавление MDFeedBack.
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
        /// Редактирование MDFeedBack по id.
        /// </summary>
        /// <param name="id">Укажите id для изменения MDFeedBack в базе MSSQL.</param>
        /// <param name="mdFeedBackModel">Укажите тело запроса.</param>
        /// <returns>Возвращает IHttpActionResult.</returns>
        [Route("api/MDFeedBacks/{id}")]
        [HttpPut]
        public IHttpActionResult EditMDFeedBack(string id, [FromBody]MDFeedBackModel mdFeedBackModel)
        {
            if (!(int.TryParse(id, out int mdFeedBackModelId)))
                return BadRequest("Ошибка запроса. Некорректно указано поле id.");

            if (mdFeedBackModel == null)
                return BadRequest("Ошибка запроса. Некорректно указано тело запроса.");

            if (mdFeedBackModelId != mdFeedBackModel.MDFeedBackModelId)
                return BadRequest("Ошибка запроса. Некорректно указано тело запроса.");

            _mdFeedBackContext.Entry(mdFeedBackModel).State = EntityState.Modified;
            _mdFeedBackContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Удаление MDFeedBack по id.
        /// </summary>
        /// <param name="id">Укажите id для удаления MDFeedBack из базы MSSQL.</param>
        /// <returns>Возвращает IHttpActionResult.</returns>
        [Route("api/MDFeedBacks/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteMDFeedBack(string id)
        {
            if (!(int.TryParse(id, out int mdFeedBackModelId)))
                return BadRequest("Ошибка запроса. Некорректно указано поле id.");

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