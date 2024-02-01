use bevy::prelude::*;

use crate::common::util;
use lazy_static::lazy_static;
use std::sync::{Arc, Mutex};

lazy_static! {
    static ref START_SCREEN: Arc<Mutex<NodeBundle>> = Arc::new(Mutex::new(NodeBundle {
        background_color: BackgroundColor(util::hex_to_rgb("30183C")),
        ..Default::default()
    }));
}

/*

{
    let a = START_SCREEN.lock()
    if a.is_ok() {
        let mut a = a.unwrap();
        a.key = value
    }
}

*/
