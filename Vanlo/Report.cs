using System;
using System.Collections.Generic;

using RestSharp;

namespace Vanlo
{
  public class Report : Resource
  {
    public string id { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public string status { get; set; }
    public string url { get; set; }
    public DateTime? url_expires_at { get; set; }

    /// <summary>
    /// Retrieve a Report from its id and type.
    /// </summary>
    /// <param name="type">Type of report, e.g. shipment, tracker, payment_log, etc.</param>
    /// <param name="id">String representing a report.</param>
    /// <returns>Vanlo.Report instance.</returns>
    public static Report Retrieve(string type, string id)
    {
      Request request = new Request("reports/{type}/{id}");
      request.AddUrlSegment("id", id);
      request.AddUrlSegment("type", type);

      return request.Execute<Report>();
    }

    /// <summary>
    /// Create a Report.
    /// </summary>
    /// <param name="parameters">
    /// Optional dictionary containing parameters to create the carrier account with. Valid pairs:
    ///   * {"start_date", string} Date to start the report at.
    ///   * {"end_date", string} Date to end the report at.
    /// All invalid keys will be ignored.
    /// </param>
    /// <returns>Vanlo.Report instance.</returns>
    public static Report Create(string type, Dictionary<string, object> parameters = null)
    {
      Request request = new Request("reports/{type}", Method.POST);
      request.AddUrlSegment("type", type);
      request.AddQueryString(parameters ?? new Dictionary<string, object>());

      return request.Execute<Report>();
    }

    /// <summary>
    /// Get a paginated list of reports.
    /// </summary>
    /// Optional dictionary containing parameters to filter the list with. Valid pairs:
    ///   * {"before_id", string} String representing a Report ID. Only retrieve Reports created before this id. Takes precedence over after_id.
    ///   * {"after_id", string} String representing a Report ID. Only retrieve Reports created after this id.
    ///   * {"start_datetime", string} ISO 8601 datetime string. Only retrieve Reports created after this datetime.
    ///   * {"end_datetime", string} ISO 8601 datetime string. Only retrieve Reports created before this datetime.
    ///   * {"page_size", int} Max size of list. Default to 20.
    /// All invalid keys will be ignored.
    /// <param name="parameters">
    /// </param>
    /// <returns>Instance of Vanlo.ReportList.</returns>
    public static ReportList List(string type, Dictionary<string, object> parameters = null)
    {
      Request request = new Request("reports/{type}");
      request.AddUrlSegment("type", type);
      request.AddQueryString(parameters ?? new Dictionary<string, object>());

      ReportList reportList = request.Execute<ReportList>();
      reportList.filters = parameters;
      reportList.type = type;
      return reportList;
    }
  }
}
