use bevy::prelude::*;

use lazy_static::lazy_static;
use std::sync::{Arc, Mutex};

lazy_static! {
    static ref CardView: NodeBundle = NodeBundle {
        background_color: BackgroundColor("f5f5f5".to_color()),
        ..Default::default()
    };
    static ref Alert: NodeBundle = NodeBundle {
        background_color: BackgroundColor("30183C".to_color()),
        ..Default::default()
    };
}
