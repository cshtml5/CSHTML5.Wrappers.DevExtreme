using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeScriptDefinitionsSupport;
using CSHTML5.Wrappers.DevExtreme.Common;

namespace CSHTML5.Wrappers.DevExtreme.Common
{
    public static class Utils
    {
        public static JSObject ToJSObject(object o)
        {
            string serialized = JsonConvert.SerializeObject(o);
            //Windows.UI.Xaml.MessageBox.Show(serialized);
            object json = Interop.ExecuteJavaScript("JSON.parse($0)", serialized);
            return new JSObject(json);
        }

        public static JSObject CSharpEnumerableToJSObject(IEnumerable cSharpEnumerable, List<string> columnsPropertyPaths, bool forceAddEvenNullValues = false)
        {
            var jsArray = Interop.ExecuteJavaScript("[]");

            // Traverse the enumerable:
            foreach (var cSharpItem in cSharpEnumerable)
            {
                var jsItem = CSharpObjectJSObject(cSharpItem, columnsPropertyPaths, forceAddEvenNullValues);
                Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsItem);
            }

            return new JSObject(jsArray);
        }

        public static object CSharpObjectJSObject(object cSharpObject, List<string> columnsPropertyPaths, bool forceAddEvenNullValues = false)
        {
            var jsObject = Interop.ExecuteJavaScript("new Object()");

            foreach (string columnPropertyPath in columnsPropertyPaths)
            {
                // Read the property value:
                object propertyValue = GetNestedPropertyValue(cSharpObject, columnPropertyPath);

                // Get the property name:
                string propertyName;
                if (columnPropertyPath.LastIndexOf(".") > -1)
                    propertyName = columnPropertyPath.Substring(columnPropertyPath.LastIndexOf(".") + 1);
                else
                    propertyName = columnPropertyPath;

                if (propertyValue != null)
                {
                    string propertyValueString = propertyValue.ToString();
                    Interop.ExecuteJavaScript(@"$0[$1] = $2;", jsObject, propertyName, propertyValueString);
                }
                else if (forceAddEvenNullValues)
                {
                    Interop.ExecuteJavaScript(@"$0[$1] = $2;", jsObject, propertyName, null);
                }
            }

            return jsObject;
        }

        public static JSObject ConvertPlainDataSourceToJson(IEnumerable<Dictionary<string, object>> newValue, List<string> propertyNames, string itemsIdNameInJSON = null)
        {
            object jsArray = Interop.ExecuteJavaScript("[]");

            foreach (var cSharpItem in newValue)
            {
                var jsObject = Interop.ExecuteJavaScript("new Object()");
                foreach (string propertyName in propertyNames)
                {
                    string value = (cSharpItem[propertyName] ?? "").ToString();
                    Interop.ExecuteJavaScript(@"$0[$1] = $2;", jsObject, propertyName, value);
                }
                if (!string.IsNullOrWhiteSpace(itemsIdNameInJSON))
                {
                    string value = (cSharpItem[itemsIdNameInJSON] ?? "").ToString();
                    Interop.ExecuteJavaScript(@"$0[$1] = $2;", jsObject, itemsIdNameInJSON, value);
                }
                Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsObject);
            }
            return new JSObject(jsArray);
        }

        public static List<Dictionary<string, object>> ConvertEnumerableToPlainDataSource(IEnumerable enumerable, List<string> columnsPropertyPaths, out Dictionary<int, object> idsToCSharpObjects, out Dictionary<object, int> CSharpObjectsToIds, string itemsIdNameInJSON)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            int currentId = 0;
            idsToCSharpObjects = new Dictionary<int, object>();
            CSharpObjectsToIds = new Dictionary<object, int>();
            // Traverse the enumerable:
            foreach (var item in enumerable)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (string columnPropertyPath in columnsPropertyPaths)
                {
                    // Read the property value:
                    object propertyValue = Utils.GetNestedPropertyValue(item, columnPropertyPath);

                    // Get the property name:
                    string propertyName;
                    if (columnPropertyPath.LastIndexOf(".") > -1)
                        propertyName = columnPropertyPath.Substring(columnPropertyPath.LastIndexOf(".") + 1);
                    else
                        propertyName = columnPropertyPath;

                    // Add the property name and value:
                    if(!dictionary.ContainsKey(propertyName))
                    {
                        dictionary.Add(propertyName, propertyValue);
                    }
                }

                //add and remember the id associated to the item:
                idsToCSharpObjects[currentId] = item;
                CSharpObjectsToIds[item] = currentId;
                dictionary.Add(itemsIdNameInJSON, currentId);
                ++currentId;
                result.Add(dictionary);
            }

            return result;
        }

        public static object GetNestedPropertyValue(object obj, string path)
        {
            return GetNestedPropertyValue(obj, path.Split('.'));
        }

        static object GetNestedPropertyValue(object obj, string[] path)
        {
            object value = null;

            var prop = obj.GetType().GetProperty(path[0]);
            if (prop != null)
            {
                value = prop.GetValue(obj);
                if (path.Length > 1)
                    value = GetNestedPropertyValue(value, path.Skip(1).ToArray());
            }

            return value;
        }

        public static IEnumerable<Type> GetItemTypes(this IEnumerable e)
        {
            return e.GetType()
                    .GetInterfaces()
                    .Where(t => t.IsGenericType
                        && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    .Select(t => t.GetGenericArguments()[0]);
        }

        public static Type GetItemType(this IEnumerable enumerable)
        {
            Type itemType = null;
            var genericEnumerable = enumerable.Cast<object>();

            if (genericEnumerable.Count() > 0)
                itemType = genericEnumerable.First().GetType();
            else
            {
                var itemTypes = enumerable.GetItemTypes();
                if (itemTypes.Count() == 1)
                    itemType = itemTypes.First();
            }

            return itemType;
        }
    }
}
