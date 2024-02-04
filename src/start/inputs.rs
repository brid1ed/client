use std::thread::spawn;

use crate::common::color;
use crate::common::util::HexToRgb;
use bevy::ecs::component::{Component, ComponentStorage};
use bevy::prelude::*;
#[derive(Component)]
pub struct TextInput {
    placeholder: String,
}

#[derive(Component)]
pub struct Button {
    text: String,
    color: Color,
    bg_color: Color,
}
impl Default for Button {
    fn default() -> Self {
        Button {
            text: "text".to_string(),
            color: "ffffff".to_color(),
            bg_color: color::GRAY.9.to_color(),
        }
    }
}

pub struct InputPlugin;
impl Plugin for InputPlugin {
    fn build(&self, app: &mut App) {
        app.add_systems(Startup, setup)
            .add_systems(Update, text_system)
            .add_systems(Update, button_system);
    }
}

fn setup(mut commands: Commands, asset_server: Res<AssetServer>) {
    commands
        // .spawn();
        .spawn(NodeBundle::default())
        .insert(Button {
            text: "sup".to_string(),
            bg_color: color::GRAY.9.to_color(),
            color: color::WHITE.to_color(),
        });
}

fn text_system(mut query: Query<&TextInput, With<TextInput>>) {
    for text_input in &mut query {
        todo!()
    }
}

fn button_system(
    mut commands: Commands,
    mut query: Query<(&Button, Entity), With<Button>>,
    asset_server: Res<AssetServer>,
) {
    // println!("sup");
    for (button, entity) in &mut query {
        let mut component = commands.entity(entity);
        // component.child
        component.clear_children();
        component.with_children(|parent| {
            parent
                .spawn(NodeBundle {
                    background_color: BackgroundColor(button.bg_color),
                    style: Style {
                        padding: UiRect::axes(Val::Px(30.), Val::Px(10.)),
                        justify_content: JustifyContent::SpaceBetween,
                        ..default()
                    },
                    ..default()
                })
                .with_children(|parent: &mut ChildBuilder<'_, '_, '_>| {
                    parent.spawn((
                        TextBundle::from_section(
                            &button.text,
                            TextStyle {
                                font: asset_server.load("fonts/Intro.ttf"),
                                font_size: 30.0,
                                color: button.color,
                                ..default()
                            },
                        )
                        .with_style(Style {
                            align_self: AlignSelf::Center,
                            justify_self: JustifySelf::Center,
                            ..default()
                        }),
                        Label,
                    ));
                });
        });
    }
}
