import React from 'react';
import {StyleSheet, Text, TouchableOpacity} from 'react-native';

const SelectButton = ({
  label,
  value,
  onSelectValue,
  selectedValue,
  selectedColor,
  labelStyle,
  style,
}) => {
  return (
    <TouchableOpacity
      onPress={() => onSelectValue(value)}
      style={[
        styles.default,
        style,
        selectedValue === value ? {backgroundColor: selectedColor} : {},
      ]}>
      <Text
        style={[labelStyle, selectedValue === value ? {color: 'white'} : {}]}>
        {label}
      </Text>
    </TouchableOpacity>
  );
};

export default SelectButton;

const styles = StyleSheet.create({
  default: {
    padding: 5,
    elevation: 5,
    backgroundColor: 'white',
    marginHorizontal: 5,
  },
});
