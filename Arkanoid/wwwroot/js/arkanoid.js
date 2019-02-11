var canvas = $("#arkanoid");
var context = $(canvas)[0].getContext("2d");

/* Keys */
var K_LEFT = 37;
var K_RIGHT = 39;

/* Canvas */
var canvas_width = $(canvas).width();
var canvas_height = $(canvas).height();
var canvas_left_offset = $(canvas).offset().left;
var canvas_right_offset = $(canvas).offset().right;

/* Ball */
var ball_x;
var ball_y;
var ball_r = 5; 
var ball_dx;
var ball_dy; 

/* Bricks */
var bricks = []; 
var brick_rows = 5;
var brick_cols = 10;
var brick_width = (canvas_width / brick_cols) - 1;
var brick_height = 15;
var brick_padding = 1;
var destroyed_bricks = 0;
var num_of_bricks = brick_rows * brick_cols;

/* Pad */
var pad_x = canvas_width / 2;
var pad_width = 75;
var pad_height = 15;

/* Other */
var player_lives = 3;
var player_score = 0;
var row_height = brick_height + brick_padding;
var col_width = brick_width + brick_padding;
var is_ball_out = false;
var is_game_over = false;
var is_game_won = false;

function display_ball(x, y, r)
{
	context.beginPath();
	context.arc(x, y, r, 0, Math.PI * 2, true);
	context.closePath();
	context.fill();
};

function display_rect(x, y, w, h)
{
	context.beginPath();
	context.rect(x, y, w, h);
	context.closePath();
	context.fill();
}

function display_pad()
{
	display_rect(pad_x, canvas_height - pad_height, pad_width, pad_height);
}

function display_text(text, size, x, y, is_centered)
{
	context.font = size + "px Verdana";
	if (is_centered) {
		context.textAlign = "center";
	}
	context.fillText(text, x, y);
	context.textAlign = "left";
}

function display_lives()
{
	if (player_lives === 3) {
		display_ball(10, canvas_height - (ball_r * 2), ball_r + 5);
		display_ball(10, canvas_height - (ball_r * 6), ball_r + 5);
		display_ball(10, canvas_height - (ball_r * 10), ball_r + 5);
	} else if (player_lives === 2) {
		display_ball(10, canvas_height - (ball_r * 2), ball_r + 5);
		display_ball(10, canvas_height - (ball_r * 6), ball_r + 5);
	} else {
		display_ball(10, canvas_height - (ball_r * 2), ball_r + 5);
	}	
}

/* Afficher le score du joueur */
function display_score()
{
	display_text(player_score, 25, canvas_width - 100, canvas_height - 20, false); 
}

/* Afficher les briques */
function display_bricks()
{
	for (var i = 0; i < brick_rows; i++) {
		for (var j = 0; j < brick_cols; j++) {
			if (bricks[i][j] === 1) {
				display_rect(j * (brick_width + brick_padding), i * (brick_height + brick_padding), brick_width, brick_height);
			}
		}
	}
}

/* Afficher l'ecran titre (titlescreen) */
function display_ts()
{
	display_text("Arkanoid", 31, canvas_width / 2, canvas_height / 2, true);
	display_text("Press Left Mouse Button to play.", 21, canvas_width / 2, canvas_height - 150, true); 
}

function display_gameover()
{
	clear_screen();
	display_text("Game Over", 28, canvas_width / 2, canvas_height / 2, true);
	display_text("You lost. Reload this page to play again.", 21, canvas_width / 2, canvas_height / 3, true);
}

function display_victorys()
{
	clear_screen();
	display_text("Victory!", 28, canvas_width / 2, canvas_height / 2, true);
    display_text("You win!. Reload this page to play again.", 21, canvas_width / 2, canvas_height / 3, true);
}

function clear_screen()
{
	context.clearRect(0, 0, canvas_width, canvas_height);
}

function init_bricks_array()
{
	for (var x = 0; x < brick_rows; x++) {
		bricks[x] = [];
		for (var y = 0; y < brick_cols; y++) {
			bricks[x][y] = 1;
		}
	}
}

function init_ball()
{
	ball_x = canvas_width / 2;
	ball_y = canvas_height / 2;

	ball_dx = 2;
	ball_dy = 4;
}


function game_init()
{
	clear_screen(); 

	init_ball();
	init_bricks_array();
	
	game_loop();
}

function draw_current_frame()
{
	clear_screen();
	display_ball(ball_x, ball_y, ball_r);
	display_pad();
	display_bricks();
	display_lives();
	display_score();
}

function handle_events()
{
	$(document).keydown(function(e) {
		switch (e.keyCode) {
		case K_LEFT:
			pad_x -= 25;
			break;
		case K_RIGHT:
			pad_x += 25;
			break;
		}
	});

	$(document).mousemove(function(e) {

		pad_x = e.pageX - canvas_left_offset - pad_width / 2;
	});
}

function move_ball()
{
	var current_ball_row = Math.floor((ball_y - ball_r) / row_height); 
	var current_ball_col = Math.floor(ball_x / col_width);

	if (current_ball_row < brick_rows && current_ball_row >= 0 &&
	    current_ball_col < brick_cols && current_ball_col >= 0) {
		if (bricks[current_ball_row][current_ball_col] === 1) {
			ball_dy = -ball_dy; 
			bricks[current_ball_row][current_ball_col] = 0; 
			player_score += 100;
			destroyed_bricks += 1;
		}
	}

	if (ball_x + ball_dx > canvas_width || ball_x + ball_dx < 0) {
		ball_dx = -ball_dx;	
	}

	if (ball_y + ball_dy < 0) {
		ball_dy = -ball_dy;
    } else if (ball_y + ball_dy + ball_r > canvas_height - pad_height) {

		if (ball_x + ball_r > pad_x && ball_x - ball_r < pad_x + pad_width) {
			ball_dx = 12 * ((ball_x - (pad_x + pad_width / 2)) / pad_width); 
			ball_dy = -ball_dy;
		} else {
			is_ball_out = true;
		}
	}

	ball_x += ball_dx;
	ball_y += ball_dy;

	if (is_ball_out && player_lives <= 3) {
		player_lives--;
		is_ball_out = false;
		init_ball();
	}

	if (player_lives === 0) {
		is_game_over = true;
	}

	if (destroyed_bricks === num_of_bricks) {
		is_game_won = true;
	}
}

display_ts();
canvas.click(game_init); 
handle_events();

function game_loop(timer)
{
	
	if (is_game_over) {
		display_gameover();
	} else if (is_game_won) {
		display_victorys();
	} else {
		move_ball();
		draw_current_frame();
	}

	window.requestAnimationFrame(game_loop);
}
