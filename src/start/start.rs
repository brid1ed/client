use crate::common::color;
use crate::common::components::text_field;
use crate::common::util::HexToRgb;
use bevy::prelude::*;

pub struct StartPlugin;

impl Plugin for StartPlugin {
    fn build(&self, app: &mut App) {
        app.add_plugins(text_field::TextFieldPlugin)
            .add_systems(Startup, setup);
    }
}

fn setup(mut commands: Commands, asset_server: Res<AssetServer>) {
    commands
        .spawn(NodeBundle::default())
        .insert(text_field::TextField {
            text: "adsf".to_string(),
            padding_horizontal: 20.0,
            padding_vertical: 12.0,
            ..Default::default()
        });
}
