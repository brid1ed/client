use crate::common::util::{self, HexToRgb};
use crate::start::inputs;
use bevy::prelude::*;

pub struct StartPlugin;

impl Plugin for StartPlugin {
    fn build(&self, app: &mut App) {
        app.add_plugins(inputs::InputPlugin)
        // .add_systems(Startup, startup)
        ;
    }
}

fn startup(mut commands: Commands) {
    commands.spawn(NodeBundle {
        background_color: BackgroundColor("ffffff".to_color()),
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
