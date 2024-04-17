using Common.Domain.Assets;

namespace CaptainSonar.Common.Domain.Assets
{
    public abstract class Asset(AssetType assetType, AssetName assetName, int size, bool isConsumable = true)
    {
        public readonly bool IsConsumable = isConsumable;
        public readonly AssetName AssetName = assetName;
        public readonly AssetType AssetType = assetType;
        public AssetSlots Slots = new(size);
    }
}
