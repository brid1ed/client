use bevy::prelude::*;

use lazy_static::lazy_static;
use std::sync::{Arc, Mutex};

lazy_static! {
    static ref CardView: Arc<Mutex<NodeBundle>> = Arc::new(Mutex::new(NodeBundle {
        background_color: BackgroundColor("f5f5f5".to_color())),
        ..Default::default()
    }));
    static ref Alert: Arc<Mutex<NodeBundle>> = Arc::new(Mutex::new(NodeBundle {
        background_color: BackgroundColor("30183C".to_color()),
        ..Default::default()
    }));
}
