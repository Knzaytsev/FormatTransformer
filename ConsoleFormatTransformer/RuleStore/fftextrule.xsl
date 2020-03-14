<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="text" indent="yes"/>
	<xsl:template match="/">
      <xsl:text>id	parent	name	tag</xsl:text>
      <xsl:value-of select="'&#13;'" />
      <xsl:apply-templates select="annotation/text" />
   </xsl:template>
	<xsl:template match="text">
		<xsl:value-of select="concat(@id, '	', @parent, '	', @name, '	', @tag, '&#13;')"/>
	</xsl:template>
	
</xsl:stylesheet>
