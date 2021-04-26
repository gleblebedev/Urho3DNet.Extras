using Urho3DNet.InputEvents;

namespace Urho3DNet.Samples
{
    public class SampleList : AbstractGameScreen
    {
        private readonly SharedPtr<UIElement> listViewHolder_ = new SharedPtr<UIElement>();
        private readonly ListView _list;

        public SampleList(Context context) : base(context)
        {
            UIRoot.SetDefaultStyle(ResourceCache.GetResource<XMLFile>("UI/DefaultStyle.xml"));

            var layout = UIRoot.CreateChild<UIElement>();
            listViewHolder_.Value = layout;
            layout.LayoutMode = LayoutMode.LmVertical;
            layout.SetAlignment(HorizontalAlignment.HaCenter, VerticalAlignment.VaCenter);
            layout.Size = new IntVector2(300, 600);
            layout.SetStyleAuto();

            _list = layout.CreateChild<ListView>();
            _list.MinSize = new IntVector2(300, 600);
            _list.SelectOnClickEnd = true;
            _list.HighlightMode = HighlightMode.HmAlways;
            _list.SetStyleAuto();
            _list.Name = "SampleList";

            DefaultFogColor = new Color(0.1f, 0.2f, 0.4f, 1.0f);

            MouseMode = MouseMode.MmFree;
            IsMouseVisible = true;
        }

        public void Add<T>() where T : Sample
        {
            var button = Context.CreateObject<Button>();
            button.MinHeight = 30;
            button.SetStyleAuto();
            button.SetVar("SampleType", typeof(T).Name);

            var title = button.CreateChild<Text>();
            title.SetAlignment(HorizontalAlignment.HaCenter, VerticalAlignment.VaCenter);
            title.SetText(typeof(T).Name);
            title.SetFont(Context.ResourceCache.GetResource<Font>("Fonts/Anonymous Pro.ttf"), 30);
            title.SetStyleAuto();

            _list.AddItem(button);
        }
    }
}