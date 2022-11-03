import React from 'react';
import SelectButton from '../../utilities/SelectButton';
import Colors from '../../settings/Colors';

const CategoryItem = ({
  item,
  index,
  selectedCategory,
  handleCategoryFilter,
  btnStyle,
  btnLabelStyle,
}) => {
  return (
    <>
      <SelectButton
        key={index}
        label={item.name}
        style={btnStyle}
        labelStyle={btnLabelStyle}
        selectedColor={Colors.gray}
        value={item.id}
        selectedValue={selectedCategory}
        onSelectValue={handleCategoryFilter}
      />
    </>
  );
};

export default CategoryItem;
