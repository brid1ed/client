use bevy::ecs::component::{Component, ComponentStorage};
use bevy::prelude::*;
use std::thread::spawn;

use crate::common::color;
use crate::common::util::HexToRgb;

pub struct TextInputFieldPlugin;
impl Plugin for TextInputFieldPlugin {
    fn build(&self, app: &mut App) {
        app.add_systems(Update, system);
    }
}

#[derive(Component)]
pub struct TextInputField {
    pub padding_horizontal: f32,
    pub padding_vertical: f32,

    pub hint: String,
    pub text_size: f32,
    pub hint_color: Color,
    pub text_color: Color,
    pub font_family: String,

    pub bg_color: Color,
}

impl Default for TextInputField {
    fn default() -> Self {
        TextInputField {
            padding_horizontal: 0.0,
            padding_vertical: 0.0,

            hint: "".to_string,
            text_size: 20.0,
            hint_color: color::GRAY.9.to_color,
            text_color: color::WHITE.to_color,
            font_family: "Medium".to_string,

            bg_color: color::BLACK.to_color,
        }
    }
}

fn system(
    mut commands: Commands,
    mut query: Query<(&TextInputField, Entity), With<TextInputField>>,
    asset_server: Res<AssetServer>,
) {
    for (item, entity) in &mut query {
        let mut component = commands.entity(entity);

        component.clear_children();
        component.with_children(|parent| {
            parent
                .spawn(NodeBundle {
                    background_color: BackgroundColor(item.bg_color),
                    style: Style {
                        padding: UiRect::axes(
                            Val::Px(item.padding_horizontal),
                            Val::Px(item.padding_vertical),
                        ),
                        justify_content: JustifyContent::SpaceBetween,
                        ..default()
                    },
                    ..Default::default()
                })
                .with_children(|parent: &mut ChildBuilder<'_, '_, '_>| {
                    parent.spawn((
                        TextBundle::from_section(
                            &item.text,
                            TextStyle {
                                font: asset_server.load(
                                    "fonts/Pretendard-".to_string() + &item.font_family + ".ttf",
                                ),
                                font_size: item.text_size,
                                color: item.text_color,
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
