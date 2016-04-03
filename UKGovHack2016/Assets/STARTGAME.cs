

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Security.Permissions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class STARTGAME : MonoBehaviour
{

    public GameObject button; // Assign in inspector
    public string location_url = "http://datapoint.metoffice.gov.uk/public/data/val/wxobs/all/xml/sitelist?key=d5486145-d5fe-4cdc-8112-f391eb43e794";
    //private string [,,] locations = new string["","",""][];
    public float gps_latitude = 0.00f;
    public float gps_longitude = 0.00f;
    private string nearest_station;
    public string weather_type = "clear";
    public string day_type = "day";

    // Use this for initialization
    IEnumerator Start()
    {
        float temp_dist = 0.00f;
        float smallest_dist = 9999999f;
        int id = 0;
        // WWW location_data = new WWW(location_url);
        // yield return location_data;

        // while (location_data.isDone) { }

        if (Input.location.isEnabledByUser){
            Input.location.Start();
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }
            if (maxWait < 1){
                Debug.Log("Location Access Timed out");
            }
            else {
                gps_latitude = Input.location.lastData.latitude;
                gps_longitude= Input.location.lastData.longitude;
                Debug.Log("Loca tion Access was successful");
                
            }
            Input.location.Stop();
        }else
        {
            Debug.Log("Location sensing is not enabled :'(");
        }
        Debug.Log(gps_latitude);
        Debug.Log(gps_longitude);


        System.Net.HttpWebRequest request = System.Net.WebRequest.Create(location_url) as System.Net.HttpWebRequest;
        System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse;
        XmlDocument location = new XmlDocument();
        location.Load(response.GetResponseStream());

        //XmlDocument location = new XmlDocument();
        //location.LoadXml(location_data.text);

        XmlNodeList locations = location.GetElementsByTagName("Location");
        Debug.Log(locations[0].Attributes["longitude"].Value);
        for (int i = 0; i <= locations.Count-1; i++)
        {
            
            temp_dist = Mathf.Sqrt((Mathf.Pow((gps_longitude - float.Parse(locations[i].Attributes["longitude"].Value)), 2.0f) + Mathf.Pow((gps_latitude - float.Parse(locations[i].Attributes["latitude"].Value)), 2.0f)));
            if (temp_dist < smallest_dist)
            {
                id = i;
                smallest_dist = temp_dist;
                nearest_station = locations[i].Attributes["id"].Value;
                Debug.Log(temp_dist);
            }
        }
        Debug.Log(locations[id].Attributes["longitude"].Value);
        string template= "http://datapoint.metoffice.gov.uk/public/data/val/wxobs/all/xml/{0}?res=hourly&key=d5486145-d5fe-4cdc-8112-f391eb43e794"; 
        string weather_url = string.Format(template, nearest_station);
        Debug.Log(weather_url);
        //string weather_url = "http://datapoint.metoffice.gov.uk/public/data/val/wxobs/all/xml/[nearest_station]?res=hourly&key=d5486145-d5fe-4cdc-8112-f391eb43e794";
        //WWW weather_data = new WWW(weather_url);

       // yield return weather_data;
        // while (weather_data.isDone) { }

        System.Net.HttpWebRequest requestw = System.Net.WebRequest.Create(weather_url) as System.Net.HttpWebRequest;
        System.Net.HttpWebResponse responsew = requestw.GetResponse() as System.Net.HttpWebResponse;
        XmlDocument weather_doc = new XmlDocument();
        weather_doc.Load(responsew.GetResponseStream());

      //  XmlDocument weather_doc = new XmlDocument();
       // weather_doc.LoadXml(weather_data.text);

        XmlNodeList weather = weather_doc.GetElementsByTagName("Rep");
        int weather_id = int.Parse(weather[0].Attributes["W"].Value);
        switch (weather_id)
        {
            case 0:
                day_type = "night";
                break;
            case 1:
				day_type = "day";
                break;
            case 2:
                weather_type = "cloudy";
                day_type = "night";
                break;
            case 3:
                weather_type = "cloudy";
                break;
            case 5:
                weather_type = "fog";
                break;
            case 6:
                weather_type = "fog";
                break;
            case 7:
                weather_type = "cloudy";
                break;
            case 8:
                weather_type = "cloudy";
                break;
            case 9:
                weather_type = "rain";
                day_type = "night";
                break;
            case 10:
                weather_type = "rain";
                break;
            case 11:
                weather_type = "rain";
                break;
            case 12:
                weather_type = "rain";
                break;
            case 13:
                weather_type = "rain";
                day_type = "night";
                break;
            case 14:
                weather_type = "rain";
                break;
            case 15:
                weather_type = "rain";
                break;
            default: /* Optional */
                weather_type = "clear";
                day_type = "day";
                break;
        }
        //System.Console.WriteLine(weather_type);
        //System.Console.WriteLine(day_type);

	}

    // Update is called once per frame
    void Update()
    {
		GameObject.Find("Score").GetComponent<Text>().text = day_type + " " + weather_type;

		GameObject.Find("Location").GetComponent<Text>().text = "Your Location: " + gps_latitude + "," + gps_longitude;

		if (day_type.Length == "night".Length) {
			GameObject.Find ("car").GetComponent<weatherControlScript> ().SetNight ();
		} else {
			GameObject.Find ("car").GetComponent<weatherControlScript> ().SetDay ();
		}

		if (weather_type.Length == "rain".Length) {
			GameObject.Find ("car").GetComponent<weatherControlScript> ().SetRain ();
		} else if (weather_type.Length == "cloudy".Length) {
			GameObject.Find ("car").GetComponent<weatherControlScript> ().SetClouds ();
		} else if (weather_type.Length == "fog".Length) {
			GameObject.Find ("car").GetComponent<weatherControlScript> ().SetFog ();
		}

    }

    public void gameStart()
    {
        button.SetActive(false);
    }

	public void restartGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}
}

