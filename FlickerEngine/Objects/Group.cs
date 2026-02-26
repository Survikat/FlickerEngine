namespace FlickerEngine.Objects;

public class Group : Basic {
    private List<Basic> Members = new();
    
    public int Count => Members.Count;
    public int IndexOf(Basic Member) => Members.IndexOf(Member);
    
    public bool Contains(Basic Member) => Members.Contains(Member);
    
    public void Add(Basic Member) => Members.Add(Member);
    public void Remove(Basic Member) => Members.Remove(Member);
    public void Clear() => Members.Clear();

    public void ForEach(Action<Basic> Action) {
        List<Basic> CurrentMembers = Members.ToList();
        CurrentMembers.ForEach(Action);
    }
    
    public void ForEachExists(Action<Basic> Action) {
        List<Basic> ExistingMembers = Members.Where(Member => Member.Alive).ToList();
        ExistingMembers.ForEach(Action);
    }
    
    public override void Draw() {
        base.Draw();
        
        ForEachExists(basic => basic.Draw());
    }

    public override void Update(float Delta) {
        base.Update(Delta);
        
        ForEachExists(basic => basic.Update(Delta));
    }

    /// <summary>
    /// Kills all members contained within the Group, along with its self.
    /// </summary>
    public override void Kill() {
        base.Kill();
        
        for (int i = 0; i < Count; i++)
            Members[i].Kill();
    }
    
    public override void Dispose() {
        base.Dispose();
        
        Kill();
        ForEach(Member => Member.Dispose());
        Clear();
    }
}