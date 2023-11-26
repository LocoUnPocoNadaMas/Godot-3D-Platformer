extends CharacterBody3D

@export var Speed: float = 4.0
@export var JumpVelocity: float = 8.0

@export var Gravity: float = 20.0

var _facingAngle: float = 0.0

var _model: MeshInstance3D

const SPEED = 5.0
const JUMP_VELOCITY = 4.5

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")

func _ready():
	_model = get_node("ModelPlayer")

func _physics_process(delta: float):
	# Apply gravity if it in the air
	if !is_on_floor():
		Fall(delta)
	# If it on the floor, it can Jump
	else:
		Jump()
	# Get key/button pressed
	var input = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down");
	#var dir0 = new Vector3(input.X, 0, input.Y);
	Move(input);
	if input != Vector2.ZERO:
		Rotate(input)
	
func Move(input: Vector2):
	# Assign direction to velocity
	velocity.z = input.y * Speed
	velocity.x = input.x * Speed
	move_and_slide()
	
func Rotate(input: Vector2):
	# Set facing direction
	_facingAngle = Vector2(input.y , input.x).angle()
	#var aux = _model.rotation
	# Clunky Rotation
	# aux.Y = _facingAngle;
	
	# Smooth Rotation
	#aux.Y = Mathf.LerpAngle(aux.Y, _facingAngle, 0.5f)
	# Rotate
	_model.rotation.y = lerp_angle(_model.rotation.y, _facingAngle, 0.5)
	
func Jump():
	if Input.is_action_pressed("jump"):
		velocity.y = JumpVelocity;
	
func Fall(delta: float):
	velocity.y -= Gravity * delta;
	if global_position.y < -5:
		GameOver()
	
func GameOver():
	#get_tree().reload_current_scene()
	EventBus.RestartEventHandler.emit()
