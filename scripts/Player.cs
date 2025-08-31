using Godot;

public partial class Player : Node2D
{
    [Export] public float Speed { get; set; } = 300f;
    [Export] public Vector2 Size { get; set; } = new Vector2(64, 64);

    private Vector2 _velocity = Vector2.Zero;

    public override void _Process(double delta)
    {
        _velocity = Vector2.Zero;
        if (Input.IsActionPressed("ui_left"))  _velocity.X -= 1;
        if (Input.IsActionPressed("ui_right")) _velocity.X += 1;
        if (Input.IsActionPressed("ui_up"))    _velocity.Y -= 1;
        if (Input.IsActionPressed("ui_down"))  _velocity.Y += 1;

        if (_velocity.LengthSquared() > 0)
            _velocity = _velocity.Normalized();

        Position += _velocity * Speed * (float)delta;

        var vp = GetViewportRect().Size;
        var half = Size * 0.5f;
        Position = new Vector2(
            Mathf.Clamp(Position.X, half.X, vp.X - half.X),
            Mathf.Clamp(Position.Y, half.Y, vp.Y - half.Y)
        );

        QueueRedraw();
    }

    public override void _Draw()
    {
        // Dibuja un rectángulo azul centrado en el nodo
        DrawRect(new Rect2(-Size / 2f, Size), Colors.Blue, filled: true);
    }
}
