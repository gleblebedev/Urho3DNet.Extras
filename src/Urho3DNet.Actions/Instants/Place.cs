namespace Urho3DNet.Actions
{
    public class Place : ActionInstant
    {
        public Vector3 Position { get; }

        protected internal override ActionState StartAction(Object target)
        {
            return new PlaceState(this, target);
        }

        #region Constructors

        public Place(Vector3 pos)
        {
            Position = pos;
        }

        public Place(int posX, int posY, int posZ = 0)
        {
            Position = new Vector3(posX, posY, posZ);
        }

        #endregion Constructors
    }

    public class PlaceState : ActionInstantState
    {
        public PlaceState(Place action, Object target)
            : base(action, target)
        {
            if (Target is Node node)
                node.Position = action.Position;
        }
    }
}