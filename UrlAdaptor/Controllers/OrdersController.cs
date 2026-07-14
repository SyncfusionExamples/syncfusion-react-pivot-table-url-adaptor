using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlAdaptor.Models;

namespace UrlAdaptor.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {

        // method to retrieve data
        [HttpGet]
        [Route("api/[controller]")]
        public List<OrdersDetails> GetOrderData()
        {
            // Retrieve all records and convert to list
            var data = OrdersDetails.GetAllRecords().ToList();
            return data;
        }

        // method to handle incoming data manager requests
        [HttpPost]
        [Route("api/[controller]")]
        public object Post()
        {
            // Retrieve data source and convert to queryable
            IQueryable<OrdersDetails> DataSource = GetOrderData().AsQueryable();

            // Get total record count
            int totalRecordsCount = DataSource.Count();

            // Return result and total record count
            return new { result = DataSource, count = totalRecordsCount };
        }

        /// <summary>
        /// Inserts a new data item into the data collection.
        /// </summary>
        /// <param name="newRecord">It contains the new record detail that needs to be inserted.</param>
        /// <returns>Returns void</returns>
        [HttpPost]
        [Route("api/[controller]/Insert")]
        public void Insert([FromBody] CRUDModel<OrdersDetails> newRecord)
        {
            if (newRecord.value != null)
            {
                OrdersDetails.GetAllRecords().Insert(0, newRecord.value);
            }
        }

        /// <summary>
        /// Update an existing data item from the data collection.
        /// </summary>
        /// <param name="updatedRecord">It contains the updated record detail that needs to be updated.</param>
        /// <returns>Returns void</returns>
        [HttpPost]
        [Route("api/[controller]/Update")]
        public void Update([FromBody] CRUDModel<OrdersDetails> updatedRecord)
        {
            var updatedOrder = updatedRecord.value;
            var data = OrdersDetails.GetAllRecords().FirstOrDefault(existingOrder => existingOrder.OrderID == updatedOrder.OrderID);
            if (data != null)
            {
                // Update the existing record.
                data.OrderID = updatedOrder.OrderID;
                data.CustomerID = updatedOrder.CustomerID;
                // data.ShipCity = updatedOrder.ShipCity;
                // data.ShipCountry = updatedOrder.ShipCountry;
                data.EmployeeID = updatedOrder.EmployeeID;
                data.Freight = updatedOrder.Freight;
                // data.Verified = updatedOrder.Verified;
                // data.OrderDate = updatedOrder.OrderDate;
                // data.ShipName = updatedOrder.ShipName;
                // data.ShipCountry = updatedOrder.ShipCountry;
                // data.ShippedDate = updatedOrder.ShippedDate;
                // data.ShipAddress = updatedOrder.ShipAddress;
            }
        }

        /// <summary>
        /// Remove a specific data item from the data collection.
        /// </summary>
        /// <param name="deletedRecord">It contains the specific record detail that needs to be removed.</param>
        /// <returns>Returns void</returns>
        [HttpPost]
        [Route("api/[controller]/Remove")]
        public void Remove([FromBody] CRUDModel<OrdersDetails> deletedRecord)
        {
            int orderId = int.Parse(deletedRecord.key.ToString());
            var data = OrdersDetails.GetAllRecords().FirstOrDefault(orderData => orderData.OrderID == orderId);
            if (data != null)
            {
                // Remove the record from the data collection.
                OrdersDetails.GetAllRecords().Remove(data);
            }
        }

        [HttpPost]
        [Route("api/[controller]/CrudUpdate")]
        public void CrudUpdate([FromBody] CRUDModel<OrdersDetails> request)
        {
            if (request.action == "update")
            {
                var orderValue = request.value;
                OrdersDetails existingRecord = OrdersDetails.GetAllRecords().Where(or => or.OrderID == orderValue.OrderID).FirstOrDefault();
                existingRecord.OrderID = orderValue.OrderID;
                existingRecord.CustomerID = orderValue.CustomerID;
                existingRecord.ShipCity = orderValue.ShipCity;
                existingRecord.ShipCountry = orderValue.ShipCountry;
                existingRecord.EmployeeID = orderValue.EmployeeID;
                existingRecord.Freight = orderValue.Freight;
                existingRecord.Verified = orderValue.Verified;
                existingRecord.OrderDate = orderValue.OrderDate;
                existingRecord.ShipName = orderValue.ShipName;
                existingRecord.ShipCountry = orderValue.ShipCountry;
                existingRecord.ShippedDate = orderValue.ShippedDate;
                existingRecord.ShipAddress = orderValue.ShipAddress;
            }
            else if (request.action == "insert")
            {
                OrdersDetails.GetAllRecords().Insert(0, request.value);
            }
            else if (request.action == "remove")
            {
                OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(or => or.OrderID == int.Parse(request.key.ToString())).FirstOrDefault());
            }
        }
    }

    /// <summary>
    /// Generic model for handling CRUD operations from the Pivot Table.
    /// The Pivot Table uses this structure to send data to Insert, Update, and Remove endpoints.
    /// </summary>
    /// <typeparam name="T">The data type (e.g., SalesData)</typeparam>
    public class CRUDModel<T> where T : class
    {
        /// <summary>
        /// Action being performed (e.g., 'Add', 'Edit', 'Delete').
        /// Indicates the type of operation requested by the client.
        /// </summary>
        public string? action { get; set; }

        /// <summary>
        /// Primary key column name (e.g., 'orderid').
        /// Identifies which column is used as the unique identifier.
        /// </summary>
        public string? keyColumn { get; set; }

        /// <summary>
        /// The primary key value (e.g., the OrderID).
        /// Used to identify the specific record being operated on.
        /// </summary>
        public object? key { get; set; }

        /// <summary>
        /// The single record being operated on (for Insert, Update operations).
        /// Contains all field values for the record.
        /// </summary>
        public T? value { get; set; }

        /// <summary>
        /// Additional parameters sent by the client.
        /// Can contain extra metadata or configuration options.
        /// </summary>
        public IDictionary<string, object>? @params { get; set; }
    }
}