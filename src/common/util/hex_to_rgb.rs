use bevy::prelude::Color;

pub trait HexToRgb {
    fn to_color(&self) -> Color;
}

impl HexToRgb for &'static str {
    fn to_color(&self) -> Color {
        Color::hex(self).unwrap()
    }
}

impl HexToRgb for String {
    fn to_color(&self) -> Color {
        Color::hex(self).unwrap()
    }
}
