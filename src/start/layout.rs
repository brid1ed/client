use bevy::prelude::*;

use crate::common::util::{self, hex_to_rgb};
use lazy_static::lazy_static;
use std::sync::{Arc, Mutex};

lazy_static! {
    pub static ref StartScreen: NodeBundle = NodeBundle {
        background_color: BackgroundColor(util::hex_to_rgb("30183C")),
        style: Style {
            width: Val::Px(100.0),
            height: Val::Px(30.0),
            ..Default::default()
        },
        ..Default::default()
    };
}

pub struct LayoutPlugin;

impl Plugin for LayoutPlugin {
    fn build(&self, app: &mut App) {
        app.add_systems(Startup, startup);
    }
}

fn startup(mut commands: Commands) {
    commands.spawn(NodeBundle {
        background_color: BackgroundColor(hex_to_rgb("ffffff")),
        style: Style {
            width: Val::Px(480.0),
            height: Val::Px(120.0),
            align_self: AlignSelf::Center,
            justify_self: JustifySelf::Center,
            ..Default::default()
        },
        ..Default::default()
    });
}
