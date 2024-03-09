use bevy::ecs::component::{Component, ComponentStorage};
use bevy::input::mouse::MouseButtonInput;
use bevy::prelude::*;
use bevy::render::render_resource::ShaderType;
use std::thread::spawn;

use crate::common::color;
use crate::common::util::HexToRgb;

pub struct TextFieldPlugin;
impl Plugin for TextFieldPlugin {
    fn build(&self, app: &mut App) {
        app.add_systems(Update, system)
            .add_systems(Update, click_event);
    }
}

#[derive(Component)]
pub struct TextField {
    pub padding_horizontal: f32,
    pub padding_vertical: f32,

    pub text: String,
    pub text_size: f32,
    pub text_color: Color,
    pub font_family: String,

    pub bg_color: Color,

    pub on_click: fn(isize),
}

impl Default for TextField {
    fn default() -> Self {
        TextField {
            padding_horizontal: 0.0,
            padding_vertical: 0.0,

            text: "".to_string(),
            text_size: 20.0,
            text_color: color::WHITE.to_color(),
            font_family: "Medium".to_string(),

            bg_color: color::BLACK.to_color(),

            on_click: |_| {},
        }
    }
}

fn click_event(
    mut commands: Commands,
    mut query: Query<(&TextField, &Transform), With<TextField>>,
    mouse: Res<ButtonInput<MouseButton>>,
    windows: Query<&Window>,
) {
    let window = windows.single();
    let width = window.resolution.width();
    let height = window.resolution.height();
    let Some(mut cursor_position) = window.cursor_position() else {
        return;
    };

    for (item, transform) in query.iter() {
        let pos = transform.translation;
        let scale = transform.scale;

        if (cursor_position.x >= pos.x - scale.x
            && cursor_position.x <= pos.x + scale.x
            && cursor_position.y >= pos.y - scale.y
            && cursor_position.y <= pos.y + scale.y
            && mouse.just_pressed(MouseButton::Left))
        {
            println!("text field clicked");
        }
    }
}

fn system(
    mut commands: Commands,
    mut query: Query<(&TextField, Entity), With<TextField>>,
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
                .with_children(|parent: &mut ChildBuilder<'_>| {
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
