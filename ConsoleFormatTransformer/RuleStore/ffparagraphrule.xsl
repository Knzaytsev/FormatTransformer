<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="text" indent="yes"/>
	<xsl:template match="/">
      <xsl:text>id</xsl:text>

      <xsl:apply-templates select="annotation/text/paragraphs" />
   </xsl:template>
	<xsl:template match="paragraph">
		<xsl:value-of select="concat(@id, '&#13;')"/>
	</xsl:template>
	
</xsl:stylesheet>