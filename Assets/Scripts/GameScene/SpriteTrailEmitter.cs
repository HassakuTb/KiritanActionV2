using UnityEngine;

class SpriteTrailEmitter : MonoBehaviour{
    public SpriteRenderer Target;
    public Material Material;
    public int LifetimeFrames;
    public Color Color;
    public AnimationCurve AlphaOverLifetime = AnimationCurve.Linear(0, 1, 1, 0);
    public string SortingLayerName = "Default";
    public int OrderInLayer;

    private GameObject trailParent;

    private void Start() {
        this.trailParent = new GameObject("TrailContainer");
    }

    private void Emit() {
        GameObject trailObj = new GameObject("trail");
        trailObj.transform.position = Target.transform.position;
        trailObj.transform.rotation = Target.transform.rotation;
        trailObj.transform.localScale = Target.transform.lossyScale;
        trailObj.transform.SetParent(this.trailParent.transform);

        SpriteTrail trail = trailObj.AddComponent<SpriteTrail>();
        trail.Init(this);
    }

    private void Update() {
        Emit();
    }

    public void DestroyTrail() {
        Destroy(this.trailParent);
    }

    public void DestroyTrailImmediate() {
        DestroyImmediate(this.trailParent);
    }
}


[RequireComponent(typeof(SpriteRenderer))]
class SpriteTrail : MonoBehaviour {
    
    private int lifetimeFrames;
    private AnimationCurve alphaOverLifetime;
    private int currentTime = 0;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(SpriteTrailEmitter emitter) {
        this.lifetimeFrames = emitter.LifetimeFrames;
        this.alphaOverLifetime = emitter.AlphaOverLifetime;

        this.spriteRenderer.sprite = emitter.Target.sprite;
        this.spriteRenderer.material = emitter.Material;
        this.spriteRenderer.color = emitter.Color;
        this.spriteRenderer.sortingLayerName = emitter.SortingLayerName;
        this.spriteRenderer.sortingOrder = emitter.OrderInLayer;
    }

    private void Update() {
        Color col = this.spriteRenderer.color;
        float alpha = this.alphaOverLifetime.Evaluate(this.currentTime / (float)this.lifetimeFrames);
        this.spriteRenderer.color = new Color(col.r, col.g, col.b, alpha);

        this.currentTime++;
        if (this.currentTime > this.lifetimeFrames) {
            Destroy(this.gameObject);
        }

    }
}
