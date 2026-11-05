namespace CubeTracker.UI.Behaviours;

public sealed class DragHandler
{
  private readonly Form _form;
  private bool _dragging;
  private Point _startCursor;
  private Point _startLocation;

  public event Action<Point>? DragCompleted;

  public DragHandler(Form form)
  {
    _form = form;
    Attach(form);
  }

  public void Attach(Control control)
  {
    control.MouseDown += OnMouseDown;
    control.MouseMove += OnMouseMove;
    control.MouseUp += OnMouseUp;

    foreach (Control child in control.Controls) Attach(child);
  }

  private void OnMouseDown(object? sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Left) return;
    _dragging = true;
    _startCursor = Control.MousePosition;
    _startLocation = _form.Location;
  }

  private void OnMouseMove(object? sender, MouseEventArgs e)
  {
    if (!_dragging) return;
    var now = Control.MousePosition;
    _form.Location = new Point(_startLocation.X + now.X - _startCursor.X, _startLocation.Y + now.Y - _startCursor.Y);
  }

  private void OnMouseUp(object? sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Left || !_dragging) return;
    _dragging = false;
    DragCompleted?.Invoke(_form.Location);
  }
}