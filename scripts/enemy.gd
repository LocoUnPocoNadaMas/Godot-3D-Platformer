extends Area3D

@export var _moveSpeed: float = 2
@export var _moveDirection: Vector3

var _startPosition: Vector3
var _targetPosition: Vector3

# Called when the node enters the scene tree for the first time.
func _ready():
	_startPosition = global_position
	_targetPosition = _startPosition + _moveDirection

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float):
	global_position = global_position.move_toward(_targetPosition, _moveSpeed * delta)
	if global_position == _targetPosition:
		if global_position == _startPosition:
			_targetPosition = _startPosition + _moveDirection;
		else:
			_targetPosition = _startPosition;

func OnBodyEntered(body: Node3D):
	if body.is_in_group(""):
		pass
	body.GameOver()
