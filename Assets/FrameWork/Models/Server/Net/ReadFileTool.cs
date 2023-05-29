/******************************************************************************
 *
 * Maintaince Logs:
 * 2013-01-06   WP      Initial version. 
 * 2013-01-10   WP      Added:FetchLevelByTXTFile(),FetchLevel(),FetchItem()
 * *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

/// <summary>
/// use for external call
/// </summary>
public class ReadFileTool
{
	static public T JsonToClass<T>(string json) where T : class
	{
		T t = JsonReader.Deserialize<T>(json);
		return t;
	}

    static public string JsonByObject(object value)
    {
        string t = JsonWriter.Serialize(value);

        return t;
    }
}
