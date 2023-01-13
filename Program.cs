using System.Diagnostics;
using SyncHuduSyncro.Hudu;
using SyncHuduSyncro.Hudu.Models;
using SyncHuduSyncro.Syncro;
using System.Configuration;

var syncroUrl = ConfigurationManager.AppSettings.Get("Syncro_URL");
var sw = new Stopwatch();

sw.Start();
Console.WriteLine("Caching all Syncro Assets");
var syncro = new SyncroAPI();
syncro.UpdateAssetList();
syncro.UpdateContactsList();
Console.WriteLine($"Successfully Cached {syncro.AllAssets.Count} Assets and {syncro.AllContacts.Count} Contacts from Syncro. Duration: {sw.ElapsedMilliseconds / 1000} Seconds.");

sw.Stop();
sw.Reset();

sw.Start();
Console.WriteLine("Caching Hudu Data");
var hudu = new HuduAPI();
hudu.UpdateAssetList();
Console.WriteLine($"Successfully Cached {hudu.AllAssets.Count} Assets from Hudu. Duration: {sw.ElapsedMilliseconds / 1000} Seconds.");
sw.Stop();
sw.Reset();

Console.WriteLine("Syncro<->Hudu Sync Process Beginning");
sw.Start();
foreach (var syncroDevice in syncro.AllAssets)
{
    var huduDevice = hudu.AllAssets.FirstOrDefault(I => I.cards.Any(c => c.sync_id == syncroDevice.id));
    if (huduDevice == null)
        continue;


    //Update syncro asset to link to hudu. Only run if not set/has changed
    if (string.IsNullOrEmpty(syncroDevice.properties.Hudu) || syncroDevice.properties.Hudu != syncroUrl)    
        syncro.SetHuduAssetLink(syncroDevice.id, $"{syncroUrl}/a/{huduDevice.slug}");

    //Try to get asset -> contact relation from syncro and re-create it on Hudu
    //  Note: uses asset field relations inside Hudu, NOT relation API.
    HuduAsset? huduContact = null;
    var syncroContact = syncro.AllContacts.FirstOrDefault(I => I.id == syncroDevice.contact_id);
    if (syncroContact != null)
        huduContact = hudu.AllAssets.FirstOrDefault(I => I.cards.Any(c => c.sync_id == syncroContact.id));

    //TODO: Only update hudu asset when the syncro asset has been updated (name or relation)
    //      For now, only run if the name is not updated (won't account for re-assigning device without renaming it)
    //      Digging into custom asset fields for comparison is needed but I don't want to work on that right now.

    if (huduDevice.name != syncroDevice.name)
    {
        //Enforce assets to be named the same in Hudu as on Syncro (Syncro as authoritative friendly names)
        huduDevice.name = syncroDevice.name;

        //Update the asset inside hudu. Optionally add contact relation.
        hudu.UpdateHuduAsset(new HuduUpdateAssetRequest(huduDevice, huduContact));
    }    
}
sw.Stop();
Console.WriteLine($"Completed Syncro<->Hudu Sync Process. Duration: {sw.ElapsedMilliseconds / 1000} Seconds.");