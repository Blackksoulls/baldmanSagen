/// @description Insert description here
// You can write your code in this editor

vkey_pressed = false; hkey_pressed = false;
collide_bottom = instance_position(x, y+17, o_collide) || instance_position(x+16, y+17, o_collide);
collide_top = instance_position(x, y-1, o_collide) || instance_position(x+16, y-1, o_collide);
collide_left = instance_position(x-1, y, o_collide) || instance_position(x-1, y+16, o_collide);
collide_right = instance_position(x+17, y, o_collide) || instance_position(x+17, y+16, o_collide);

if (collide_top) {
	vspeed = 0;
} else {
	while (instance_position(x, y-1+vspeed, o_collide) || instance_position(x+16, y-1+vspeed, o_collide)) {
		if (vspeed >= 1) {
			vspeed = round(vspeed * decrease_factor);
		} else {
			vspeed = 0;	
		}
	}
}

if (collide_bottom) {
	vspeed = 0;
} else {
	while (instance_position(x, y+17+vspeed, o_collide) || instance_position(x+16, y+17+vspeed, o_collide)) {
		if (vspeed >= 1) {
			vspeed = round(vspeed * decrease_factor);
		} else {
			vspeed = 0;	
		}
	}
}

if (collide_left) {
	hspeed = 0;
} else {
	while (instance_position(x+hspeed-1, y, o_collide) || instance_position(x+hspeed-1, y+16, o_collide)) {
		if (hspeed >= 1) {
			hspeed = round(hspeed * decrease_factor);
		} else {
			hspeed = 0;	
		}
	}
}

if (collide_right) {
	hspeed = 0;
} else {
	while (instance_position(x+16+hspeed+1, y, o_collide) || instance_position(x+16+hspeed+1, y+16, o_collide)) {
		if (hspeed >= 1) {
			hspeed = round(hspeed * decrease_factor);
		} else {
			hspeed = 0;	
		}
	}
}

if (!keyboard_check(k_left) || !keyboard_check(k_right)) {
	if (keyboard_check(k_left) && !(instance_position(x-1, y, o_collide) || instance_position(x-1, y+16, o_collide))) {
		if (power(hspeed, 2) < power(smax, 2)) hspeed = hspeed - speed_augment;
		hkey_pressed = true;
	}

	if (keyboard_check(k_right) && !(instance_position(x+17, y, o_collide) || instance_position(x+17, y+16, o_collide))) {
		if (power(hspeed, 2) < power(smax, 2)) hspeed = hspeed + speed_augment;
		hkey_pressed = true;
	}
} else {
	hspeed = round(hspeed * decrease_factor);
}

if (!keyboard_check(k_up) || !keyboard_check(k_down)) {
	if (keyboard_check(k_up) && !(instance_position(x, y-1, o_collide) || instance_position(x+16, y-1, o_collide))) {
		if (power(vspeed, 2) < power(smax, 2)) vspeed = vspeed - speed_augment;
		vkey_pressed = true;
	}

	if (keyboard_check(k_down) && !(instance_position(x, y+17, o_collide) || instance_position(x+16, y+17, o_collide))) {
		if (power(vspeed, 2) < power(smax, 2)) vspeed = vspeed + speed_augment;
		vkey_pressed = true;
	}	
} else {
	vspeed = round(vspeed * decrease_factor);
}

if (keyboard_check(ord("F"))) {
	room_restart();
}

if (hkey_pressed == false) {
	if (power(hspeed, 2) > smin) {
	hspeed = round(hspeed * decrease_factor);
	} else {
		hspeed = 0;	
	}
}

if (vkey_pressed == false) {
	if (power(vspeed, 2) > smin) {
		vspeed = round(vspeed * decrease_factor);
	} else {
		vspeed = 0;	
	}
}