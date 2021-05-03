using System;
using System.Collections.Generic;
using System.Linq;

namespace Urho3DNet.Editor
{
    public class AbstractGizmo : IDisposable, IGizmo
    {
        private readonly Context _context;
        private readonly StaticModel _staticModel;
        private readonly SharedPtr<Node> _gizmoNode;
        private readonly SharedPtr<Material> _material;

        public AbstractGizmo(Node parentNode):this(parentNode.Context)
        {
            _gizmoNode.Value.Parent = parentNode;
        }

        public AbstractGizmo(Context context)
        {
            _context = context;
            _gizmoNode = new Node(context);
            _gizmoNode.Value.Name = "Gizmo";
            _staticModel = _gizmoNode.Value.CreateComponent<StaticModel>();
            _staticModel.IsEnabled = false;
            _staticModel.ViewMask = 0x80000000;
            _staticModel.IsOccludee = false;
            _material = new Material(_context);
            _material.Value.SetTechnique(0, _context.ResourceCache.GetResource<Technique>("Techniques/NoTextureOverlay.xml"));
            _material.Value.PixelShaderDefines = "VERTEXCOLOR";
            _material.Value.VertexShaderDefines = "VERTEXCOLOR";
            Color = Color.White;
        }

        public Context Context => _context;


        public Vector3 Position
        {
            get
            {
                return _gizmoNode.Value.Position;
            }
            set
            {
                _gizmoNode.Value.Position = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return _gizmoNode.Value.Rotation;
            }
            set
            {
                _gizmoNode.Value.Rotation = value;
            }
        }

        public Vector3 WorldToLocal(Vector3 pos)
        {
            return _gizmoNode.Value.WorldToLocal(pos);
        }

        public Vector3 WorldToLocal(Vector4 vec)
        {
            return _gizmoNode.Value.WorldToLocal(vec);
        }

        public Vector3 LocalToWorld(Vector3 pos)
        {
            return _gizmoNode.Value.LocalToWorld(pos);
        }

        public Vector3 LocalToWorld(Vector4 pos)
        {
            return _gizmoNode.Value.LocalToWorld(pos);
        }

        public Vector3 Scale
        {
            get
            {
                return _gizmoNode.Value.GetScale();
            }
            set
            {
                _gizmoNode.Value.SetScale(value);
            }
        }

        public Color Color
        {
            get
            {
                return _material.Value.GetShaderParameter("MatDiffColor").Color;
            }
            set
            {
                _material.Value.SetShaderParameter("MatDiffColor", value);
            }
        }

        public Model Model
        {
            get
            {
                return _staticModel.GetModel();
            }
            set
            {
                _staticModel.SetModel(value);
                if (value != null)
                {
                    _staticModel.SetMaterial(_material);
                }
            }
        }

        public bool IsEnabled => _staticModel.IsEnabled;

        public void Hide()
        {
            _staticModel.IsEnabled = false;
        }

        public void Show(Scene scene)
        {
            _staticModel.IsEnabled = true;
            scene.GetComponent<Octree>().AddManualDrawable(_staticModel);
        }

        public virtual void ResizeGizmo(Camera camera)
        {
            float scale = 0.1f / camera.Zoom;

            if (camera.IsOrthographic)
                scale *= camera.OrthoSize;
            else
                scale *= (camera.View * _gizmoNode.Value.Position).Z;

            _gizmoNode.Value.SetScale(new Vector3(scale, scale, scale));
        }

        public virtual void Raycast(ref GizmoRaycast result)
        {
        }

        void IDisposable.Dispose()
        {
            Dispose(true);

            _gizmoNode.Dispose();
            _material.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {

        }



        protected unsafe VertexBuffer CreateModel(Vector3[] vertices, PrimitiveType primitiveType,
            ushort[] indexData)
        {
            return CreateModel(vertices.Select(_ => new PositionColor(_, Color.White)).ToArray(), primitiveType,
                indexData);
        }
        protected unsafe VertexBuffer CreateModel(PositionColor[] vertices, PrimitiveType primitiveType, ushort[] indexData)
        {
            var fromScratchModel = new Model(Context);
            var vb = new VertexBuffer(Context);
            var ib = new IndexBuffer(Context);
            var geom = new Geometry(Context);
            // Shadowed buffer needed for raycasts to work, and so that data can be automatically restored on device loss
            vb.IsShadowed = true;
            // We could use the "legacy" element bitmask to define elements for more compact code, but let's demonstrate
            // defining the vertex elements explicitly to allow any element types and order
            var elements = new VertexElementList();
            elements.Add(new VertexElement(VertexElementType.TypeVector3, VertexElementSemantic.SemPosition));
            elements.Add(new VertexElement(VertexElementType.TypeVector4, VertexElementSemantic.SemColor));

            vb.SetSize((uint)vertices.Length, elements);

            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var positionColor in vertices)
            {
                min.X = Math.Min(positionColor.Position.X, min.X);
                min.Y = Math.Min(positionColor.Position.Y, min.Y);
                min.Z = Math.Min(positionColor.Position.Z, min.Z);
                max.X = Math.Max(positionColor.Position.X, max.X);
                max.Y = Math.Max(positionColor.Position.Y, max.Y);
                max.Z = Math.Max(positionColor.Position.Z, max.Z);
            }

            UpdateVertexBuffer(vb, vertices);

            ib.IsShadowed = true;
            ib.SetSize((uint)indexData.Length, false);
            fixed (ushort* indexPtr = indexData)
            {
                ib.SetData((IntPtr)indexPtr);
            }
            geom.SetVertexBuffer(0, vb);
            geom.IndexBuffer = ib;
            geom.SetDrawRange(primitiveType, 0, (uint)indexData.Length);

            fromScratchModel.NumGeometries = 1;
            fromScratchModel.SetGeometry(0, 0, geom);
            fromScratchModel.BoundingBox = new BoundingBox(min, max);

            // Though not necessary to render, the vertex & index buffers must be listed in the model so that it can be saved properly
            var vertexBuffers = new VertexBufferRefList();
            var indexBuffers = new IndexBufferRefList();
            vertexBuffers.Add(vb);
            indexBuffers.Add(ib);
            // Morph ranges could also be not defined. Here we simply define a zero range (no morphing) for the vertex buffer
            var morphRangeStarts = new UIntArray();
            var morphRangeCounts = new UIntArray();
            morphRangeStarts.Add(0);
            morphRangeCounts.Add(0);
            fromScratchModel.SetVertexBuffers(vertexBuffers, morphRangeStarts, morphRangeCounts);
            fromScratchModel.IndexBuffers = indexBuffers;
            Model = fromScratchModel;

            return vb;
        }

        public virtual void Highlight(bool highlight)
        {

        }

        public Node Node => _gizmoNode.Value;

        protected unsafe void UpdateVertexBuffer(VertexBuffer vb, PositionColor[] vertices)
        {
            fixed (PositionColor* vertexPtr = vertices)
            {
                vb.SetData((IntPtr)vertexPtr);
            }
        }
    }
}
