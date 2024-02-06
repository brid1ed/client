use bevy::prelude::*;

use lazy_static::lazy_static;
use std::sync::{Arc, Mutex};

lazy_static! {
    static ref CardView: NodeBundle = NodeBundle {
        ..Default::default()
    };
    static ref Alert: NodeBundle = NodeBundle {
        ..Default::default()
    };
}
