using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BioInfo.App.Library.Common.Helpers
{
    //public static class FileHelper
    //{

    //    public static async Task DeleteIfExists(this string file)
    //    {
    //        StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
    //        if (await FileExists(storageFolder, file))
    //        {
    //            StorageFile sampleFile = await storageFolder.GetFileAsync(file);
    //            await sampleFile.DeleteAsync();
    //        }
    //    }
    //    public static async Task CreateFileIfNotExists(this string file, string header)
    //    {
    //        await InvocationHelper.ExecuteWithIgnoreException(async () =>
    //        {
    //            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
    //            StorageFile dataFile = null;
    //            if (!(await FileExists(storageFolder, file)))
    //            {
    //                dataFile = await storageFolder.CreateFileAsync(file);
    //            }
    //            else
    //            {
    //                dataFile = await storageFolder.GetFileAsync(file);
    //            }
    //            var readText = await Windows.Storage.FileIO.ReadTextAsync(dataFile);
    //            if (string.IsNullOrWhiteSpace(readText) && !string.IsNullOrWhiteSpace(header))
    //            {
    //                await Windows.Storage.FileIO.AppendLinesAsync(dataFile, new List<string> { header });

    //            }
    //        });
    //    }
    //    public static async Task AppendLines(this string file, IEnumerable<string> toWrite)
    //    {
    //        StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
    //        StorageFile dataFile = null;
    //        if (!(await FileExists(storageFolder, file)))
    //        {
    //            dataFile = await storageFolder.CreateFileAsync(file);
    //        }
    //        else
    //        {
    //            dataFile = await storageFolder.GetFileAsync(file);
    //        }
    //        try
    //        {
    //            await Windows.Storage.FileIO.AppendLinesAsync(dataFile, toWrite);
    //        }
    //        catch (Exception ex)
    //        {
    //            //may get data too fast. 
    //        }
    //    }
    //    public static async Task CreateIfNotThereAndWrite(this string file, string toWrite, string header = "")
    //    {
    //        StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
    //        StorageFile dataFile = null;
    //        if (!(await FileExists(storageFolder, file)))
    //        {
    //            dataFile = await storageFolder.CreateFileAsync(file);
    //        }
    //        else
    //        {
    //            dataFile = await storageFolder.GetFileAsync(file);
    //        }
    //        var readText = await Windows.Storage.FileIO.ReadTextAsync(dataFile);
    //        if (string.IsNullOrWhiteSpace(readText) && !string.IsNullOrWhiteSpace(header))
    //        {
    //            await Windows.Storage.FileIO.AppendLinesAsync(dataFile, new List<string> { header });

    //        }
    //        try
    //        {
    //            await Windows.Storage.FileIO.AppendLinesAsync(dataFile, new List<string> { toWrite });
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }

    //    public static async Task<bool> FileExists(StorageFolder storageFolder, string file)
    //    {
    //        try
    //        {
    //            await storageFolder.GetFileAsync(file);
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return false;
    //    }

    //}
}
