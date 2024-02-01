use bevy::prelude::*;
use hex;

pub fn hex_to_rgb(hex: &str) -> Color {
    let bytes = hex::decode(hex).unwrap_or(vec![0, 0, 0]);

    let r = bytes[0] as f32 / 255.0;
    let g = bytes[1] as f32 / 255.0;
    let b = bytes[2] as f32 / 255.0;
    Color::rgb(r, g, b)
}

pub trait HexToRgb {
    fn to_color(&self) -> Color;
}

impl HexToRgb for &'static str {
    fn to_color(&self) -> Color {
        let bytes = hex::decode(self).unwrap_or(vec![0, 0, 0]);
        let r = bytes[0] as f32 / 255.0;
        let g = bytes[1] as f32 / 255.0;
        let b = bytes[2] as f32 / 255.0;
        Color::rgb(r, g, b)
    }
}
